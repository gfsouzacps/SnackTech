using SnackTech.Common.Dto.Api;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters;

internal class PedidoPresenter
{
    internal static ResultadoOperacao<PedidoRetornoDto> ApresentarResultadoPedido(Pedido pedido)
    {
        var pedidoDto = ConverterParaDto(pedido);

        return new ResultadoOperacao<PedidoRetornoDto>(pedidoDto);
    }


    internal static ResultadoOperacao<Guid> ApresentarResultadoPedidoIniciado(Pedido entidade)
    {
        var guidPedido = entidade.Id;

        return new ResultadoOperacao<Guid>(guidPedido);
    }

    internal static ResultadoOperacao<List<PedidoRetornoDto>> ApresentarResultadoPedido(IEnumerable<Pedido> pedidos)
    {
        var pedidosDto = pedidos.Select(ConverterParaDto).ToList();

        return new ResultadoOperacao<List<PedidoRetornoDto>>(pedidosDto);
    }

    internal static ResultadoOperacao ApresentarResultadoOk()
    {
        return new ResultadoOperacao();
    }

    internal static PedidoRetornoDto ConverterParaDto(Pedido pedido)
    {
        return new PedidoRetornoDto
        {
            Id = pedido.Id,
            DataCriacao = pedido.DataCriacao.Valor,
            Status = pedido.Status.Valor,
            Cliente = ClientePresenter.ConverterParaDto(pedido.Cliente),
            Itens = pedido.Itens.Select(converterItemParaDto).ToList()
        };
    }

    private static PedidoItemRetornoDto converterItemParaDto(PedidoItem pedidoItem)
    {
        return new PedidoItemRetornoDto
        {
            Id = pedidoItem.Id,
            Quantidade = pedidoItem.Quantidade.Valor,
            Observacao = pedidoItem.Observacao,
            Valor = pedidoItem.Valor(),
            Produto = ProdutoPresenter.ConverterParaDto(pedidoItem.Produto)
        };
    }

    internal static Pedido ConverterParaEntidade(PedidoRetornoDto pedidoDto)
    {
        var clienteEntidade = ClientePresenter.ConverterParaEntidade(pedidoDto.Cliente);
        var itens = pedidoDto.Itens.Select(converterItemParaEntidade).ToList();
        return new Pedido(pedidoDto.Id, pedidoDto.DataCriacao, pedidoDto.Status, clienteEntidade, itens);
    }

    private static PedidoItem converterItemParaEntidade(PedidoItemRetornoDto itemDto)
    {
        var produto = ProdutoPresenter.ConverterParaEntidade(itemDto.Produto);

        return new PedidoItem(itemDto.Id, produto, itemDto.Quantidade, itemDto.Observacao);
    }
}
