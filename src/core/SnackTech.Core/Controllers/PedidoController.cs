using SnackTech.Common.Dto;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Gateways;
using SnackTech.Core.Interfaces;
using SnackTech.Core.UseCases;

namespace SnackTech.Core.Controllers;

public class PedidoController(IPedidoDataSource dataSource, IClienteDataSource clienteDataSource) : IPedidoController
{
    public async Task<ResultadoOperacao<Guid>> IniciarPedido(string? cpfCliente)
    {
        var pedidoGateway = new PedidoGateway(dataSource);
        var clienteGateway = new ClienteGateway(clienteDataSource);

        var pedido = await PedidoUseCase.IniciarPedido(cpfCliente, pedidoGateway, clienteGateway);

        return pedido;
    }
}
