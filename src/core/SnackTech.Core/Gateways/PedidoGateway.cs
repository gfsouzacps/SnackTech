using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Gateways;

public class PedidoGateway(IPedidoDataSource dataSource)
{
    internal async Task<bool> CadastrarNovoPedido(Pedido entidade)
    {
        var pedidoDto = ConverterParaDto(entidade);

        return await dataSource.InserirPedidoAsync(pedidoDto);
    }

    internal async Task<Pedido?> PesquisarPorIdentificacao(GuidValido identificacao)
    {
        var pedidoDto = await dataSource.PesquisarPorIdentificacaoAsync(identificacao);

        if (pedidoDto is null)
            return null;

        return ConverterParaEntidade(pedidoDto);
    }


    internal async Task<IEnumerable<Pedido>> PesquisarPedidosPorCliente(GuidValido clienteId)
    {
        var pedidosDto = await dataSource.PesquisarPedidosPorClienteIdAsync(clienteId);

        return pedidosDto.Select(ConverterParaEntidade);
    }

    internal async Task<IEnumerable<Pedido>> PesquisarPedidosPorStatus(StatusPedidoValido status)
    {
        var pedidosDto = await dataSource.PesquisarPedidosPorStatusAsync(status.Valor);

        return pedidosDto.Select(ConverterParaEntidade);
    }

    internal async Task<bool> AtualizarStatusPedido(Pedido pedido)
    {
        var pedidoDto = ConverterParaDto(pedido);

        return await dataSource.AlterarPedidoAsync(pedidoDto);
    }

    internal async Task<bool> AtualizarItensDoPedido(Pedido pedido)
    {
        var pedidoDto = ConverterParaDto(pedido);

        return await dataSource.AlterarItensDoPedidoAsync(pedidoDto);
    }

    internal static PedidoDto ConverterParaDto(Pedido pedido)
    {
        return new PedidoDto
        {
            Id = pedido.Id,
            DataCriacao = pedido.DataCriacao.Valor,
            Status = pedido.Status.Valor,
            Cliente = ClienteGateway.ConverterParaDto(pedido.Cliente),
            Itens = pedido.Itens.Select(ConverterItemParaDto)
        };
    }

    internal static PedidoItemDto ConverterItemParaDto(PedidoItem pedidoItem)
    {
        return new PedidoItemDto
        {
            Id = pedidoItem.Id,
            Quantidade = pedidoItem.Quantidade.Valor,
            Observacao = pedidoItem.Observacao,
            Valor = pedidoItem.Valor(),
            Produto = ProdutoGateway.ConverterParaDto(pedidoItem.Produto)
        };
    }

    internal static Pedido ConverterParaEntidade(PedidoDto pedidoDto)
    {
        return new Pedido(
            pedidoDto.Id, 
            pedidoDto.DataCriacao, 
            pedidoDto.Status, 
            ClienteGateway.converterParaEntidade(pedidoDto.Cliente), 
            pedidoDto.Itens.Select(ConverterItemParaEntidade).ToList()
        );
    }

    internal static PedidoItem ConverterItemParaEntidade(PedidoItemDto pedidoItemDto)
    {
        return new PedidoItem(
            pedidoItemDto.Id, 
            ProdutoGateway.ConverterParaEntidade(pedidoItemDto.Produto),
            pedidoItemDto.Quantidade, 
            pedidoItemDto.Observacao
        );
    }
}
