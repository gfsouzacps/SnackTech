using SnackTech.Common.Dto;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.ApiSources;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Gateways
{
    public class MercadoPagoGateway(IMercadoPagoIntegration apiMercadoPago,MercadoPagoOptions mercadoPagoOptions)
    {
        internal async Task<string> IntegrarPedido(Pedido pedidoAIntegrar){

            var pedidoDto = PedidoGateway.ConverterParaDto(pedidoAIntegrar);
            var autenticacao = await apiMercadoPago.Autenticar(mercadoPagoOptions);

            var resposta = await apiMercadoPago.GerarQrCode(autenticacao.TokenDeAcesso,mercadoPagoOptions,pedidoDto);

            return resposta.DadoDoCodigo;
        }

        internal async Task<Guid> BuscarPedidoViaOrder(string orderId){
            var autenticacao = await apiMercadoPago.Autenticar(mercadoPagoOptions);

            var resposta = await apiMercadoPago.BuscarOrdemPagamento(autenticacao.TokenDeAcesso,mercadoPagoOptions,orderId);

            return resposta;
        }
    }
}