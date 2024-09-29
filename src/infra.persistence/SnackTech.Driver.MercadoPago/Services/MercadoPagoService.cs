using Newtonsoft.Json;
using SnackTech.Common.Dto;
using SnackTech.Driver.MercadoPago.Payloads;

namespace SnackTech.Driver.MercadoPago.Services
{
    public class MercadoPagoService(IHttpClientFactory httpClientFactory, MercadoPagoOptions mercadoPagoOptions)
    {
        private readonly IHttpClientFactory httpClientFactory = httpClientFactory;
        private readonly MercadoPagoOptions mercadoPagoOptions = mercadoPagoOptions;

        public async Task<AutenticacaoResponse> Autenticar(){
            var rota = "oauth/token";
            var objetoPayload = Autenticacao.CriarPayload(mercadoPagoOptions.ClientId,mercadoPagoOptions.ClientSecret);

            var jsonContent = GerarContentParaRequisicao(objetoPayload);
            var httpClient = CriarHttpClientBase(mercadoPagoOptions.UrlBase);

            var resposta = await httpClient.PostAsync(rota,jsonContent);
            
            resposta.EnsureSuccessStatusCode();

            return await RetornarConteudo<AutenticacaoResponse>(resposta);
        }

        public async Task<PedidoResponse> EnviarPedido(string accessToken, string userId, string posId, CriarPedido novoPedido){
            var rota = $"instore/orders/qr/seller/collectors/{userId}/pos/{posId}/qrs";

            var content = GerarContentParaRequisicao(novoPedido);

            var httpClient = CriarHttpClientBase(mercadoPagoOptions.UrlBase);
            httpClient.DefaultRequestHeaders.Add("Authorization",$"Bearer {accessToken}");

            var resposta = await httpClient.PutAsync(rota,content);

            return await RetornarConteudo<PedidoResponse>(resposta);
        }

        private static async Task<T> RetornarConteudo<T>(HttpResponseMessage resposta){
            resposta.EnsureSuccessStatusCode();

            var content = await resposta.Content.ReadAsStringAsync();
            var objeto = JsonConvert.DeserializeObject<T>(content);
            return objeto ?? throw new InvalidOperationException("Objeto do pedido está nulo.");
        }

        private HttpClient CriarHttpClientBase(string urlBase){
            var httpClient = httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri(urlBase);
            httpClient.DefaultRequestHeaders.Add("Content-Type","application/json");

            return httpClient;
        }

        private static StringContent GerarContentParaRequisicao<T>(T objetoContent){
            var objetoSerializado = JsonConvert.SerializeObject(objetoContent);
            var content = new StringContent(objetoSerializado);
            return content;
        }
    }
}