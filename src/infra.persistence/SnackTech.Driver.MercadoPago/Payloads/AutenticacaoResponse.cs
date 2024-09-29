namespace SnackTech.Driver.MercadoPago.Payloads
{
    public class AutenticacaoResponse
    {
        public string access_token {get; set;} = default!;        
        public string token_type {get; set;} = default!;
        public int expires_in {get; set;}
        public string scope {get; set;} = default!;
        public int user_id {get; set;}
        public bool live_mode {get; set;}
    }
}