using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Domain.Contracts;
using SnackTech.Domain.Models;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class PedidoRepository(RepositoryDbContext repositoryDbContext) : IPedidoRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task AtualizarPedidoAsync(Pedido pedidoAtualizado)
        {
            foreach (var item in pedidoAtualizado.Itens)
            {
                var entry = _repositoryDbContext.Entry(item);
                if (entry.State == EntityState.Detached)
                {
                    entry.State = EntityState.Added;
                }
            }

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
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Status == Domain.Enums.StatusPedido.AguardandoPagamento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> PesquisarPorClienteAsync(Guid identificacaoCliente)
        {
            return await _repositoryDbContext.Pedidos
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.IdCliente == identificacaoCliente)
                .ToListAsync();
        }

        public async Task<Pedido?> PesquisarPorIdentificacaoAsync(Guid identificacao)
        {
            return await _repositoryDbContext.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id.Equals(identificacao));
        }
    }
}