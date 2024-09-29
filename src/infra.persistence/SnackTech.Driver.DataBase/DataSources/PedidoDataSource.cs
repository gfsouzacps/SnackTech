using Microsoft.EntityFrameworkCore;
using SnackTech.Common.CustomExceptions;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Driver.DataBase.Context;
using SnackTech.Driver.DataBase.Entities;
using SnackTech.Driver.DataBase.Util;
using System.Drawing;

namespace SnackTech.Driver.DataBase.DataSources;

public class PedidoDataSource(RepositoryDbContext repositoryDbContext) : IPedidoDataSource
{
    public async Task<bool> AlterarItensDoPedidoAsync(PedidoDto pedidoAtualizado)
    {
        var pedido = await repositoryDbContext.Pedidos
                .Include(p => p.Itens)
                .Where(p => p.Id == pedidoAtualizado.Id)
                .FirstOrDefaultAsync();

        if (pedido is null)
        {
            throw new PedidoRepositoryException($"Pedido com identificacao {pedidoAtualizado.Id} não encontrado no banco de dados.");
        }

        var itensNoBanco = pedido.Itens.ToDictionary(p => p.Id, p => p);

        foreach (var itemAtualizar in pedidoAtualizado.Itens)
        {
            var itemEntityAtualizar = Mapping.Mapper.Map<PedidoItem>(itemAtualizar);
            if (itensNoBanco.TryGetValue(itemEntityAtualizar.Id, out var itemBanco))
            {
                itemBanco.Quantidade = itemAtualizar.Quantidade;
                itemBanco.Valor = itemAtualizar.Valor;
                itemBanco.Observacao = itemAtualizar.Observacao;
                itemBanco.Produto = itemEntityAtualizar.Produto;
            }
            else
            {
                //adiocionando itens novos dessa forma evitasse que o EF tente criar um novo produto a partir do produto presente no item
                var entry = repositoryDbContext.Entry(itemEntityAtualizar);
                entry.State = EntityState.Added;
            }
        }

        //deletar itens que foram removidos
        var itensParaRemover = itensNoBanco.Where(i => !pedidoAtualizado.Itens.Any(p => p.Id == i.Key));
        repositoryDbContext.PedidoItens.RemoveRange(itensParaRemover.Select(i => i.Value));

        await repositoryDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AlterarPedidoAsync(PedidoDto pedidoDto)
    {
        var pedidoEntity = Mapping.Mapper.Map<Pedido>(pedidoDto);

        var resultadoItens = await AlterarItensDoPedidoAsync(pedidoDto);

        if (!resultadoItens)
        {
            return false;
        }

        repositoryDbContext.Entry(pedidoEntity).State = EntityState.Modified;
        await repositoryDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> InserirPedidoAsync(PedidoDto pedidoDto)
    {
        var pedidoEntity = Mapping.Mapper.Map<Pedido>(pedidoDto);

        //Para que o EF core não tente criar novos clientes e produtos a partir dos dados presentes no pedido
        repositoryDbContext.Entry(pedidoEntity.Cliente).State = EntityState.Unchanged;
        foreach (var item in pedidoEntity.Itens)
        {
            repositoryDbContext.Entry(item.Produto).State = EntityState.Unchanged;
        }

        repositoryDbContext.Pedidos.Add(pedidoEntity);
        var resultado = await repositoryDbContext.SaveChangesAsync();

        return resultado > 0;
    }

    public async Task<IEnumerable<PedidoDto>> PesquisarPedidosPorClienteIdAsync(Guid clienteId)
    {
        var pedidosBanco = await repositoryDbContext.Pedidos
                    .AsNoTracking()
                    .Include(p => p.Cliente)
                    .Include(p => p.Itens).ThenInclude(i => i.Produto)
                    .Where(p => p.Cliente.Id == clienteId)
                    .ToListAsync();

        return pedidosBanco.Select(Mapping.Mapper.Map<PedidoDto>);
    }

    public async Task<IEnumerable<PedidoDto>> PesquisarPedidosPorStatusAsync(int valor)
    {
        var pedidosBanco = await repositoryDbContext.Pedidos
                   .AsNoTracking()
                   .Include(p => p.Cliente)
                   .Include(p => p.Itens).ThenInclude(i => i.Produto)
                   .Where(p => (int)p.Status == valor)
                   .ToListAsync();

        return pedidosBanco.Select(Mapping.Mapper.Map<PedidoDto>);
    }

    public async Task<PedidoDto?> PesquisarPorIdentificacaoAsync(Guid identificacao)
    {
        var pedidoBanco = await repositoryDbContext.Pedidos
            .AsNoTracking()
            .Include(p => p.Cliente)
            .Include(p => p.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == identificacao);

        if (pedidoBanco == null)
        {
            return null;
        }

        return Mapping.Mapper.Map<PedidoDto>(pedidoBanco);
    }
}
