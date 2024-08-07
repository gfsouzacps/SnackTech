using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SnackTech.API.CustomResponses;
using SnackTech.Domain.DTOs.Cliente;
using SnackTech.Domain.Ports.Driving;

namespace SnackTech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController(ILogger<ClientesController> logger, IClienteService clienteService) : CustomBaseController(logger)
    {
        private readonly IClienteService clienteService = clienteService;

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<RetornoCliente>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CadastroCliente cadastroCliente)
            => await CommonExecution("Clientes.Post",clienteService.Cadastrar(cadastroCliente));

        [HttpGet]
        [Route("{cpf}")]
        [ProducesResponseType<RetornoCliente>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCpf([FromRoute] string cpf)
            => await CommonExecution("Clientes.GetByCpf",clienteService.IdentificarPorCpf(cpf));

        [HttpGet]
        [Route("cliente-padrao")]
        [ProducesResponseType<RetornoCliente>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDefaultClient()
            => await CommonExecution("Clientes.GetDefaultClient",clienteService.SelecionarClientePadrao());
    }
}