using Microsoft.AspNetCore.Mvc;
using SnackTech.API.CustomResponses;
using SnackTech.Domain.DTOs.Driving.Pedido;
using SnackTech.Domain.Ports.Driving;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace SnackTech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController(ILogger<PedidosController> logger, IPedidoService pedidoService) : CustomBaseController(logger)
    {
        private readonly IPedidoService pedidoService = pedidoService;

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("")]
        [SwaggerOperation(Summary = "Inicia um novo pedido a partir do CPF de um cliente previamente cadastrado no banco. Informe null para iniciar um novo pedido do cliente padrão")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna o identificador do novo pedido", typeof(Guid))]
        public async Task<IActionResult> IniciarPedido([FromBody] IniciarPedido pedidoIniciado)
            => await CommonExecution("Pedidos.IniciarPedido", pedidoService.IniciarPedido(pedidoIniciado.Cpf));

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("")]
        [SwaggerOperation(Summary = "Atualiza o pedido (itens anexos) que ainda não esteja no status aguardando pagamento")]
        public async Task<IActionResult> AtualizarPedido([FromBody] AtualizacaoPedido atualizacaoPedido)
            => await CommonExecution("Pedidos.AtualizarPedido", pedidoService.AtualizarPedido(atualizacaoPedido));

        [HttpPatch]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("finalizar-para-pagamento/{identificacao:guid}")]
        [SwaggerOperation(Summary = "Finaliza o pedido com o identificador informado e o coloca na situação de aguardando pagamento")]
        public async Task<IActionResult> FinalizarPedidoParaPagamento([FromRoute] string identificacao)
            => await CommonExecution("Pedidos.FinalizarPedidoParaPagamento", pedidoService.FinalizarPedidoParaPagamento(identificacao));

        [HttpGet]
        [ProducesResponseType<IEnumerable<RetornoPedido>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("aguardando-pagamento")]
        [SwaggerOperation(Summary = "Retorna a lista de pedidos com status aguardando pagamento")]
        public async Task<IActionResult> ListarPedidosParaPagamento()
            => await CommonExecution("Pedidos.ListarPedidosParaPagamento", pedidoService.ListarPedidosParaPagamento());

        [HttpGet]
        [ProducesResponseType<RetornoPedido>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{identificacao:guid}")]
        [SwaggerOperation(Summary = "Retorna o pedido com o identificador informado")]
        public async Task<IActionResult> BuscarPorIdenticacao([FromRoute] string identificacao)
            => await CommonExecution("Pedidos.ListarPedidosParaPagamento", pedidoService.BuscarPorIdenticacao(identificacao));

        [HttpGet]
        [ProducesResponseType<RetornoPedido>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("ultimo-pedido-cliente/{cpfCliente}")]
        [SwaggerOperation(Summary = "Retorna o pedido mais recente do cliente com CPF informado. Não é permitida a consulta do último pedido do cliente padrão")]
        public async Task<IActionResult> BuscarUltimoPedidoCliente([FromRoute] string cpfCliente)
            => await CommonExecution("Pedidos.ListarPedidosParaPagamento", pedidoService.BuscarUltimoPedidoCliente(cpfCliente));
    }
}