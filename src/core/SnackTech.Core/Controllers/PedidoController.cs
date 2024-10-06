using SnackTech.Common.Dto;
using SnackTech.Common.Dto.Api;
using SnackTech.Common.Interfaces.ApiSources;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Gateways;
using SnackTech.Core.Interfaces;
using SnackTech.Core.UseCases;

namespace SnackTech.Core.Controllers;

public class PedidoController(IPedidoDataSource pedidoDataSource, 
                                IClienteDataSource clienteDataSource, 
                                IProdutoDataSource produtoDataSource, 
                                IMercadoPagoIntegration mercadoPagoIntegration,
                                MercadoPagoOptions mercadoPagoOptions) : IPedidoController
{
    public async Task<ResultadoOperacao<Guid>> IniciarPedido(string? cpfCliente)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var clienteGateway = new ClienteGateway(clienteDataSource);

        var identificacaoPedido = await PedidoUseCase.IniciarPedido(cpfCliente, pedidoGateway, clienteGateway);

        return identificacaoPedido;
    }

    public async Task<ResultadoOperacao<PedidoRetornoDto>> BuscarPorIdenticacao(string identificacao)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var pedido = await PedidoUseCase.BuscarPorIdenticacao(identificacao, pedidoGateway);

        return pedido;
    }

    public async Task<ResultadoOperacao<PedidoRetornoDto>> BuscarUltimoPedidoCliente(string cpfCliente)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var clienteGateway = new ClienteGateway(clienteDataSource);

        var pedido = await PedidoUseCase.BuscarUltimoPedidoCliente(cpfCliente, pedidoGateway, clienteGateway);

        return pedido;
    }

    public async Task<ResultadoOperacao<IEnumerable<PedidoRetornoDto>>> ListarPedidosParaPagamento()
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);

        var pedidos = await PedidoUseCase.ListarPedidosParaPagamento(pedidoGateway);

        return pedidos;
    }

    public async Task<ResultadoOperacao<PedidoPagamentoDto>> FinalizarPedidoParaPagamento(string identificacao)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var mercadoPagoGateway = new MercadoPagoGateway(mercadoPagoIntegration,mercadoPagoOptions);

        var resultado = await PedidoUseCase.FinalizarPedidoParaPagamento(identificacao, pedidoGateway, mercadoPagoGateway);

        return resultado;
    }

    public async Task<ResultadoOperacao<PedidoRetornoDto>> AtualizarPedido(PedidoAtualizacaoDto pedidoAtualizado)
    {
        var pedidoGateway = new PedidoGateway(pedidoDataSource);
        var produtoGateway = new ProdutoGateway(produtoDataSource);

        var resultado = await PedidoUseCase.AtualizarItensPedido(pedidoAtualizado, pedidoGateway, produtoGateway);

        return resultado;
    }
}
