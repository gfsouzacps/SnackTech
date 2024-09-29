namespace SnackTech.Driver.MercadoPago.Payloads
{
    public class Autenticacao
    {
        public string client_secret {get; set;} = default!;
        public string client_id {get; set;} = default!;
        public string grant_type {get; set;} = default!;
        public string test_token {get; set;} = default!;

        public static Autenticacao CriarPayload(string clientId, string clientSecret)
        => new Autenticacao{
            client_id = clientId,
            client_secret = clientSecret,
            grant_type = "client_credentials",
            test_token = "false"
        };
    }
}