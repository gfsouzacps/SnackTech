using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Domain.Models;
using SnackTech.Domain.Ports.Driving;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class PedidoRepository(RepositoryDbContext repositoryDbContext) : IPedidoRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task AtualizarPedidoAsync(Pedido pedidoAtualizado)
        {
            var itensNoBanco = await _repositoryDbContext.PedidoItens
                .Where(p => p.IdPedido == pedidoAtualizado.Id)
                .ToListAsync();

            foreach (var itemAtualizar in pedidoAtualizado.Itens)
            {
                var presenteNoBanco = itensNoBanco.Any(itemBanco => itemBanco.Sequencial == itemAtualizar.Sequencial);
                var entry = _repositoryDbContext.Entry(itemAtualizar);
                
                if (presenteNoBanco && entry.State == EntityState.Detached)
                {
                    itensNoBanco.Where(i => i.Sequencial == itemAtualizar.Sequencial).First().AtualizarDadosItem(itemAtualizar.Produto, itemAtualizar.Quantidade, itemAtualizar.Observacao);
                }

                if (!presenteNoBanco && entry.State == EntityState.Detached)
                {
                    entry.State = EntityState.Added;
                }
            }

            //removendo itens que foram removidos
            foreach (var itemBanco in itensNoBanco)
            {
                if (!pedidoAtualizado.Itens.Any(i => i.Sequencial == itemBanco.Sequencial))
                {
                    _repositoryDbContext.Remove(itemBanco);
                }
            }
            
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