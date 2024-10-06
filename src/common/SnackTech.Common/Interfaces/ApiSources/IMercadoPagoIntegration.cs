
using SnackTech.Common.Dto;
using SnackTech.Common.Dto.ApiSource.MercadoPago;
using SnackTech.Common.Dto.DataSource;

namespace SnackTech.Common.Interfaces.ApiSources
{
    public interface IMercadoPagoIntegration
    {
        Task<AutenticacaoMercadoPagoDto> Autenticar(MercadoPagoOptions mercadoPagoOptions);
        Task<MercadoPagoQrCodeDto> GerarQrCode(string accessToken,MercadoPagoOptions mercadoPagoOptions, PedidoDto pedido);     
    }
}