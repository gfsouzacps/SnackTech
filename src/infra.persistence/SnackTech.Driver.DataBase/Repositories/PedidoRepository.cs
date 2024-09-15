using Microsoft.EntityFrameworkCore;
using SnackTech.Driver.DataBase.Context;
using SnackTech.Driver.DataBase.Entities;
using SnackTech.Driver.DataBase.Util;
using SnackTech.Domain.Ports.Driven;

namespace SnackTech.Driver.DataBase.Repositories
{
    public class PedidoRepository(RepositoryDbContext repositoryDbContext) : IPedidoRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task AtualizarPedidoAsync(Domain.DTOs.Driven.PedidoDto pedidoAtualizado)
        {
            var itensNoBanco = await _repositoryDbContext.PedidoItens
                .Where(p => p.Pedido.Id == pedidoAtualizado.Id)
                .ToDictionaryAsync(p => p.Id, p => p);

            foreach (var itemAtualizar in pedidoAtualizado.Itens)
            {
                var itemEntityAtualizar = Mapping.Mapper.Map<PedidoItem>(itemAtualizar);
                if (itensNoBanco.TryGetValue(itemEntityAtualizar.Id, out var itemBanco))
                {
                    itemBanco.Quantidade = itemAtualizar.Quantidade;
                    itemBanco.Valor = itemAtualizar.Valor;
                    itemBanco.Observacao = itemAtualizar.Observacao;
                    itemBanco.Produto = itemEntityAtualizar.Produto;
                    itemBanco.Sequencial = itemAtualizar.Sequencial;
                }
                else
                {
                    //adiocionando itens novos dessa forma evitasse que o EF tente criar um novo produto a partir do produto presente no item
                    var entry = _repositoryDbContext.Entry(itemEntityAtualizar);
                    entry.State = EntityState.Added;
                }
            }

            //removendo itens que foram removidos
            var itensParaRemover = itensNoBanco.Where(i => !pedidoAtualizado.Itens.Any(p => p.Id == i.Key));
            _repositoryDbContext.PedidoItens.RemoveRange(itensParaRemover.Select(i => i.Value));

            //atualizar colunas do pedido
            var pedidoEntityAtualizar = Mapping.Mapper.Map<Pedido>(pedidoAtualizado);
            _repositoryDbContext.Entry(pedidoEntityAtualizar).State = EntityState.Modified;
            _repositoryDbContext.Entry(pedidoEntityAtualizar.Cliente).State = EntityState.Unchanged;

            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task InserirPedidoAsync(Domain.DTOs.Driven.PedidoDto novoPedido)
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

        public async Task<IEnumerable<Domain.DTOs.Driven.PedidoDto>> PesquisarPedidosParaPagamentoAsync()
        {
            return (await _repositoryDbContext.Pedidos
                .AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Status == Domain.Enums.StatusPedido.AguardandoPagamento)
                .ToListAsync())
                .Select(Mapping.Mapper.Map<Domain.DTOs.Driven.PedidoDto>);
        }

        public async Task<IEnumerable<Domain.DTOs.Driven.PedidoDto>> PesquisarPorClienteAsync(Guid identificacaoCliente)
        {
            return (await _repositoryDbContext.Pedidos
                .AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Cliente.Id == identificacaoCliente)
                .ToListAsync())
                .Select(Mapping.Mapper.Map<Domain.DTOs.Driven.PedidoDto>);
        }

        public async Task<Domain.DTOs.Driven.PedidoDto?> PesquisarPorIdentificacaoAsync(Guid identificacao)
        {
            var pedido = await _repositoryDbContext.Pedidos
                .AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Itens).ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id.Equals(identificacao));

            if(pedido is null)
                return null;

            return Mapping.Mapper.Map<Domain.DTOs.Driven.PedidoDto>(pedido);
        }
    }
}