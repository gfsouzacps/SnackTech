namespace SnackTech.Driver.MercadoPago.Payloads
{
    public class MerchantOrder
    {
        public long id {get; set;}
        public string status {get; set;} = default!;
        public string external_reference {get; set;} = default!;
        //Tem mais dados no retorno, mas no momento isso atende o escopo
    }
}