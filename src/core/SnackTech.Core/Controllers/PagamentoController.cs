using Microsoft.Extensions.Options;
using SnackTech.Common.Dto;
using SnackTech.Common.Dto.Api;
using SnackTech.Common.Interfaces.ApiSources;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Gateways;
using SnackTech.Core.Interfaces;
using SnackTech.Core.UseCases;

namespace SnackTech.Core.Controllers
{
    public class PagamentoController(IPedidoDataSource pedidoDataSource, 
                                        IMercadoPagoIntegration mercadoPagoIntegration, 
                                        IOptions<MercadoPagoOptions> mercadoPagoOptions) : IPagamentoController
    {

        public async Task<ResultadoOperacao> ProcessarPagamento(PagamentoDto pagamento)
        {
            var pedidoGateway = new PedidoGateway(pedidoDataSource);
            var mercadoPagoGateway = new MercadoPagoGateway(mercadoPagoIntegration,mercadoPagoOptions.Value);

            var resultado = await PagamentoUseCase.ProcessarPagamentoEnviadoPorHook(pedidoGateway,mercadoPagoGateway,pagamento);
            
            return resultado;
        }
    }
}