using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters;

internal class PedidoPresenter
{
    internal static ResultadoOperacao<PedidoRetornoDto> ApresentarResultadoPedido(Pedido pedido)
    {
        var pedidoDto = (PedidoRetornoDto)pedido;

        return new ResultadoOperacao<PedidoRetornoDto>(pedidoDto);
    }


    internal static ResultadoOperacao<Guid> ApresentarResultadoPedidoIniciado(Pedido entidade)
    {
        var guidPedido = entidade.Id;

        return new ResultadoOperacao<Guid>(guidPedido);
    }

    internal static ResultadoOperacao<List<PedidoRetornoDto>> ApresentarResultadoPedido(IEnumerable<Pedido> pedidos)
    {
        var pedidosDto = pedidos.Select(p => (PedidoRetornoDto)p).ToList();

        return new ResultadoOperacao<List<PedidoRetornoDto>>(pedidosDto);
    }

    internal static ResultadoOperacao ApresentarResultadoOk()
    {
        return new ResultadoOperacao();
    }
}
