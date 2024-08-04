using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Adapter.DataBase.Entities;
using SnackTech.Adapter.DataBase.Util;
using SnackTech.Domain.Contracts;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class PedidoRepository(RepositoryDbContext repositoryDbContext) : IPedidoRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task AtualizarPedidoAsync(Domain.Models.Pedido pedidoAtualizado)
        {
            var pedidoEntity = Mapping.Mapper.Map<Pedido>(pedidoAtualizado);

            if (await _repositoryDbContext.Pedidos.FindAsync(pedidoEntity.Id) is Pedido found)
            {
                _repositoryDbContext.Pedidos.Entry(found).State = EntityState.Detached;
                _repositoryDbContext.Pedidos.Update(pedidoEntity);
                await _repositoryDbContext.SaveChangesAsync();
            }

            // // Carrega o pedido original do banco de dados
            // var pedidoOriginal = await _repositoryDbContext.Pedidos
            //     .Include(p => p.Itens)
            //     .FirstOrDefaultAsync(p => p.Id == pedidoAtualizado.Id);

            // if (pedidoOriginal == null)
            // {
            //     throw new Exception("O pedido informado não existe");
            // }


            //atualiza ou adiciona os itens
            // foreach (var item in pedidoAtualizado.Itens)
            // {
            //     var entry = _repositoryDbContext.Entry(item);
            //     if (entry.State == EntityState.Detached)
            //     {
            //         entry.State = EntityState.Added;
            //     }
            //     await _repositoryDbContext.SaveChangesAsync();
            // }
            
            // await _repositoryDbContext.SaveChangesAsync();

            // // Remove os itens que não estão mais no pedido
            // var itensPresentesNoBd = _repositoryDbContext.PedidoItens
            //     .Where(item => item.IdPedido == pedidoOriginal.Id)
            //     .ToList();
            // var itensRemover = itensPresentesNoBd.Where(item => !pedidoAtualizado.Itens.Any(p => p.Id == item.Id));
            // _repositoryDbContext.PedidoItens.RemoveRange(itensRemover);

            
            // Salva as alterações no banco de dados
            // await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task InserirPedidoAsync(Domain.Models.Pedido novoPedido)
        {
            var pedidoEntity = Mapping.Mapper.Map<Pedido>(novoPedido);
            _repositoryDbContext.Pedidos.Add(pedidoEntity);
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Models.Pedido>> PesquisarPedidosParaPagamentoAsync()
        {
            return (await _repositoryDbContext.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Status == Domain.Enums.StatusPedido.AguardandoPagamento)
                .ToListAsync())
                .Select(Mapping.Mapper.Map<Domain.Models.Pedido>);
        }

        public async Task<IEnumerable<Domain.Models.Pedido>> PesquisarPorClienteAsync(Guid identificacaoCliente)
        {
            return (await _repositoryDbContext.Pedidos
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Cliente.Id == identificacaoCliente)
                .ToListAsync())
                .Select(Mapping.Mapper.Map<Domain.Models.Pedido>);
        }

        public async Task<Domain.Models.Pedido?> PesquisarPorIdentificacaoAsync(Guid identificacao)
        {
            var pedido = await _repositoryDbContext.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id.Equals(identificacao));

            return Mapping.Mapper.Map<Domain.Models.Pedido>(pedido);
        }
    }
}