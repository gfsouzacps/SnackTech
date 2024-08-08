using Microsoft.EntityFrameworkCore;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Domain.Models;
using SnackTech.Domain.Ports.Driven;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class PedidoRepository(RepositoryDbContext repositoryDbContext) : IPedidoRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task AtualizarPedidoAsync(Pedido pedidoAtualizado)
        {
            var itensNoBanco = await _repositoryDbContext.PedidoItens
                .Where(p => p.IdPedido == pedidoAtualizado.Id)
                .ToDictionaryAsync(p => p.Id, p => p);

            foreach (var itemAtualizar in pedidoAtualizado.Itens)
            {
                if (itensNoBanco.TryGetValue(itemAtualizar.Id, out var itemBanco))
                {
                    itemBanco.AtualizarDadosItem(itemAtualizar.Produto, itemAtualizar.Quantidade, itemAtualizar.Observacao);
                }
                else
                {
                    //adiocionando itens novos dessa forma evitasse que o EF tente criar um novo produto a partir do produto presente no item
                    var entry = _repositoryDbContext.Entry(itemAtualizar);
                    entry.State = EntityState.Added;
                }
            }

            //removendo itens que foram removidos
            var itensParaRemover = itensNoBanco.Where(i => !pedidoAtualizado.Itens.Any(p => p.Id == i.Key));
            _repositoryDbContext.PedidoItens.RemoveRange(itensParaRemover.Select(i => i.Value));

            //atualizar colunas do pedido
            var entryPedido = _repositoryDbContext.Entry(pedidoAtualizado);
            entryPedido.State = EntityState.Modified;

            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task InserirPedidoAsync(Pedido novoPedido)
        {
            _repositoryDbContext.Pedidos.Add(novoPedido);
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pedido>> PesquisarPedidosParaPagamentoAsync()
        {
            return await _repositoryDbContext.Pedidos
                .AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Status == Domain.Enums.StatusPedido.AguardandoPagamento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> PesquisarPorClienteAsync(Guid identificacaoCliente)
        {
            return await _repositoryDbContext.Pedidos
                .AsNoTracking()
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.IdCliente == identificacaoCliente)
                .ToListAsync();
        }

        public async Task<Pedido?> PesquisarPorIdentificacaoAsync(Guid identificacao)
        {
            return await _repositoryDbContext.Pedidos
                .AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id.Equals(identificacao));
        }
    }
}