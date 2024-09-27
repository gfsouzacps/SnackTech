using System;

namespace SnackTech.Core.Controllers;

public class PedidoController(IPedidoDataSource dataSource)
{
    public async Task<ResultadoOperacao<PedidoDto>> IniciarPedido(string? cpfCliente)
    {
        var gateway = new PedidoGateway(dataSource);

        var pedido = await PedidoUseCase.IniciarPedido(cpfCliente, gateway);

        return pedido;
    }
}
