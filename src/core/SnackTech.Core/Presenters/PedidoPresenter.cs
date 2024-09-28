using System;
using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters;

internal class PedidoPresenter
{
    internal static ResultadoOperacao<Guid> ApresentarResultadoPedidoIniciado(Pedido entidade)
    {
        var guidPedido = entidade.Id;
        return new ResultadoOperacao<Guid>(guidPedido);
    }
}
