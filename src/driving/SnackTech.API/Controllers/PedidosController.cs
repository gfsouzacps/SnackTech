using Microsoft.AspNetCore.Mvc;
using SnackTech.API.CustomResponses;
using SnackTech.Domain.DTOs.Pedido;
using SnackTech.Domain.Ports.Driven;
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
        [Route("iniciar")]
        public async Task<IActionResult> IniciarPedido([FromBody] string cpfCliente)
            => await CommonExecution("Pedidos.IniciarPedido", pedidoService.IniciarPedido(cpfCliente));

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("atualizar")]
        public async Task<IActionResult> AtualizarPedido([FromBody] AtualizacaoPedido atualizacaoPedido)
            => await CommonExecution("Pedidos.AtualizarPedido", pedidoService.AtualizarPedido(atualizacaoPedido));

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("finalizar-para-pagamento")]
        public async Task<IActionResult> FinalizarPedidoParaPagamento([FromBody] string identificacao)
            => await CommonExecution("Pedidos.FinalizarPedidoParaPagamento", pedidoService.FinalizarPedidoParaPagamento(identificacao));

        [HttpGet]
        [ProducesResponseType<IEnumerable<RetornoPedido>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("aguardando-pagamento")]
        public async Task<IActionResult> ListarPedidosParaPagamento()
            => await CommonExecution("Pedidos.ListarPedidosParaPagamento", pedidoService.ListarPedidosParaPagamento());

        [HttpGet]
        [ProducesResponseType<RetornoPedido>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{identificacao:guid}")]
        public async Task<IActionResult> BuscarPorIdenticacao([FromRoute] string identificacao)
            => await CommonExecution("Pedidos.ListarPedidosParaPagamento", pedidoService.BuscarPorIdenticacao(identificacao));

        [HttpGet]
        [ProducesResponseType<RetornoPedido>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("ultimo-pedido-cliente")]
        public async Task<IActionResult> BuscarUltimoPedidoCliente([FromQuery] string cpfCliente)
            => await CommonExecution("Pedidos.ListarPedidosParaPagamento", pedidoService.BuscarUltimoPedidoCliente(cpfCliente));
    }
}