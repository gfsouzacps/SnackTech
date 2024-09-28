using System;
using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters;

internal class PedidoPresenter
{
    internal static ResultadoOperacao<PedidoDto> ApresentarResultadoPedido(Pedido pedido)
    {
        var pedidoDto = (PedidoDto)pedido;

        return new ResultadoOperacao<PedidoDto>(pedidoDto);
    }


    internal static ResultadoOperacao<Guid> ApresentarResultadoPedidoIniciado(Pedido entidade)
    {
        var guidPedido = entidade.Id;

        return new ResultadoOperacao<Guid>(guidPedido);
    }

    internal static ResultadoOperacao<List<PedidoDto>> ApresentarResultadoPedido(IEnumerable<Pedido> pedidos)
    {
        var pedidosDto = pedidos.Select(p => (PedidoDto)p).ToList();

        return new ResultadoOperacao<List<PedidoDto>>(pedidosDto);
    }

    internal static ResultadoOperacao ApresentarResultadoOk()
    {
        return new ResultadoOperacao();
    }
}
