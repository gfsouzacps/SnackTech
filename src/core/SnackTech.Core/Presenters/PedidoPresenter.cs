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

    internal static ResultadoOperacao<IEnumerable<PedidoRetornoDto>> ApresentarResultadoPedido(IEnumerable<Pedido> pedidos)
    {
        var pedidosDto = pedidos.Select(ConverterParaDto);

        return new ResultadoOperacao<IEnumerable<PedidoRetornoDto>>(pedidosDto);
    }


    internal static ResultadoOperacao<Guid> ApresentarResultadoPedidoIniciado(Pedido entidade)
    {
        var guidPedido = entidade.Id;

        return new ResultadoOperacao<Guid>(guidPedido);
    }

    internal static ResultadoOperacao<PedidoPagamentoDto> ApresentarResultadoPedido(Pedido pedido, string dadoPagamento){
        var pedidoPagamento = ConverterParaPagamentoDto(pedido,dadoPagamento);
        return new ResultadoOperacao<PedidoPagamentoDto>(pedidoPagamento);
    }

    internal static ResultadoOperacao ApresentarResultadoOk()
    {
        return new ResultadoOperacao();
    }

    internal static PedidoPagamentoDto ConverterParaPagamentoDto(Pedido pedido, string dadoPagamento){
        return new PedidoPagamentoDto{
            Id = pedido.Id,
            ValorTotal = pedido.ValorTotal,
            QrCode = dadoPagamento
        };
    }

    internal static PedidoRetornoDto ConverterParaDto(Pedido pedido)
    {
        return new PedidoRetornoDto
        {
            Id = pedido.Id,
            DataCriacao = pedido.DataCriacao.Valor,
            Status = pedido.Status.Valor,
            Cliente = ClientePresenter.ConverterParaDto(pedido.Cliente),
            Itens = pedido.Itens.Select(converterItemParaDto)
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
}
