using Microsoft.EntityFrameworkCore;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Adapter.DataBase.Entities;
using SnackTech.Adapter.DataBase.Util;
using SnackTech.Domain.Ports.Driven;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class PedidoRepository(RepositoryDbContext repositoryDbContext) : IPedidoRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task AtualizarPedidoAsync(Domain.Models.Pedido pedidoAtualizado)
        {
            var itensNoBanco = await _repositoryDbContext.PedidoItens
                .Where(p => p.Id == pedidoAtualizado.Id)
                .ToDictionaryAsync(p => p.Id, p => p);

            foreach (var itemAtualizar in pedidoAtualizado.Itens)
            {
                var itemEntityAtualizar = Mapping.Mapper.Map<PedidoItem>(itemAtualizar);
                if (!itensNoBanco.TryGetValue(itemEntityAtualizar.Id, out var itemBanco))
                {
                    
                //     // itemBanco.AtualizarDadosItem(itemAtualizar.Produto, itemAtualizar.Quantidade, itemAtualizar.Observacao);
                // }
                // else
                // {
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

        public async Task InserirPedidoAsync(Domain.Models.Pedido novoPedido)
        {
            var pedidoEntity = Mapping.Mapper.Map<Pedido>(novoPedido);

            //Para que o EF core não tente criar novos clientes e produtos a partir dos dados presentes no pedido
            _repositoryDbContext.Entry(pedidoEntity.Cliente).State = EntityState.Unchanged;
            foreach (var item in pedidoEntity.Itens)
            {
                _repositoryDbContext.Entry(item.Produto).State = EntityState.Unchanged;
            }

            _repositoryDbContext.Pedidos.Add(pedidoEntity);
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Models.Pedido>> PesquisarPedidosParaPagamentoAsync()
        {
            return (await _repositoryDbContext.Pedidos
                .AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Status == Domain.Enums.StatusPedido.AguardandoPagamento)
                .ToListAsync())
                .Select(Mapping.Mapper.Map<Domain.Models.Pedido>);
        }

        public async Task<IEnumerable<Domain.Models.Pedido>> PesquisarPorClienteAsync(Guid identificacaoCliente)
        {
            return (await _repositoryDbContext.Pedidos
                .AsNoTracking()
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Cliente.Id == identificacaoCliente)
                .ToListAsync())
                .Select(Mapping.Mapper.Map<Domain.Models.Pedido>);
        }

        public async Task<Domain.Models.Pedido?> PesquisarPorIdentificacaoAsync(Guid identificacao)
        {
            var pedido = await _repositoryDbContext.Pedidos
                .AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id.Equals(identificacao));

            if(pedido is null)
                return null;

            return Mapping.Mapper.Map<Domain.Models.Pedido>(pedido);
        }
    }
}