using SnackTech.Common.Dto.ApiSource.MercadoPago;

namespace SnackTech.Driver.MercadoPago.Payloads
{
    public class PedidoResponse
    {
        public string in_store_order_id {get; set;} = default!;
        public string qr_data {get; set;} = default!;

        public static implicit operator MercadoPagoQrCodeDto(PedidoResponse pedidoResponse){
            return new MercadoPagoQrCodeDto{
                DadoDoCodigo = pedidoResponse.qr_data
            };
        }
    }

}