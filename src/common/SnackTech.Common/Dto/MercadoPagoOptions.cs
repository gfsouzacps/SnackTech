namespace SnackTech.Common.Dto
{
    public class MercadoPagoOptions
    {
        public string UrlBase {get; set;} = default!;
        public string ClientSecret {get; set;} = default!;
        public string ClientId {get; set;} = default!;
        public string UserId {get; set;} = default!;
        public string PosId {get; set;} = default!;
    }
}