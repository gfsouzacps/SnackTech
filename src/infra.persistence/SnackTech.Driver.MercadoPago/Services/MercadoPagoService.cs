using Newtonsoft.Json;
using SnackTech.Common.Dto;
using SnackTech.Common.Dto.ApiSource.MercadoPago;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.ApiSources;
using SnackTech.Driver.MercadoPago.Payloads;

namespace SnackTech.Driver.MercadoPago.Services
{
    public class MercadoPagoService(IHttpClientFactory httpClientFactory) : IMercadoPagoIntegration
    {
        private readonly IHttpClientFactory httpClientFactory = httpClientFactory;


        public async Task<AutenticacaoMercadoPagoDto> Autenticar(MercadoPagoOptions mercadoPagoOptions){
            var rota = "oauth/token";
            var objetoPayload = Autenticacao.CriarPayload(mercadoPagoOptions.ClientId,mercadoPagoOptions.ClientSecret);

            var jsonContent = GerarContentParaRequisicao(objetoPayload);
            var httpClient = CriarHttpClientBase(mercadoPagoOptions.UrlBase);

            var resposta = await httpClient.PostAsync(rota,jsonContent);
            
            resposta.EnsureSuccessStatusCode();

            var conteudoRespostaRequisicao = await RetornarConteudo<AutenticacaoResponse>(resposta);

            AutenticacaoMercadoPagoDto dadosAutenticacao = conteudoRespostaRequisicao;
            return dadosAutenticacao;

        }

        public async Task<MercadoPagoQrCodeDto> GerarQrCode(string accessToken,MercadoPagoOptions mercadoPagoOptions, PedidoDto pedido){
            var userId = mercadoPagoOptions.UserId;
            var posId = mercadoPagoOptions.PosId;

            var pedidoMercadoPago = new CriarPedido(pedido);
            var rota = $"instore/orders/qr/seller/collectors/{userId}/pos/{posId}/qrs";

            var content = GerarContentParaRequisicao(pedidoMercadoPago);

            var httpClient = CriarHttpClientBase(mercadoPagoOptions.UrlBase);
            httpClient.DefaultRequestHeaders.Add("Authorization",$"Bearer {accessToken}");

            var resposta = await httpClient.PutAsync(rota,content);

            var conteudoResposta = await RetornarConteudo<PedidoResponse>(resposta);

            MercadoPagoQrCodeDto retorno = conteudoResposta;

            return retorno;
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