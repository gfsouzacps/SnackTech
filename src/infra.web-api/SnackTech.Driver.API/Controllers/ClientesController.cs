using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SnackTech.Driver.API.CustomResponses;
using SnackTech.Domain.DTOs.Driving.Cliente;
using SnackTech.Domain.Ports.Driving;
using Swashbuckle.AspNetCore.Annotations;

namespace SnackTech.Driver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController(ILogger<ClientesController> logger, IClienteService clienteService) : CustomBaseController(logger)
    {
        private readonly IClienteService clienteService = clienteService;

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <remarks>
        /// Cria um nova cliente no sistema.
        /// </remarks>
        /// <param name="cadastroCliente">Os dados do cliente a ser criado.</param>
        /// <returns>Um <see cref="IActionResult"/> representando o resultado da opera��o.</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<RetornoCliente>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo cliente no sistema")]
        public async Task<IActionResult> Post([FromBody] CadastroCliente cadastroCliente)
            => await CommonExecution("Clientes.Post", clienteService.Cadastrar(cadastroCliente));

        /// <summary>
        /// Retorna o cliente com o CPF informado.
        /// </summary>
        /// <remarks>
        /// Retorna as informa��es de um cliente baseado no CPF fornecido.
        /// </remarks>
        /// <param name="cpf">O CPF do cliente a ser pesquisado.</param>
        /// <returns>Um <see cref="IActionResult"/> contendo o cliente encontrado ou um erro correspondente.</returns>
        [HttpGet]
        [Route("{cpf}")]
        [ProducesResponseType<RetornoCliente>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Retorna o cliente com o CPF informado")]
        public async Task<IActionResult> GetByCpf([FromRoute] string cpf)
            => await CommonExecution("Clientes.GetByCpf", clienteService.IdentificarPorCpf(cpf));

        /// <summary>
        /// Retorna o cliente padrao.
        /// </summary>
        /// <remarks>
        /// Retorna as informa��es do cliente padr?o do sistema.
        /// </remarks>
        /// <returns>Um <see cref="IActionResult"/> contendo o cliente encontrado ou um erro correspondente.</returns>
        [HttpGet]
        [Route("cliente-padrao")]
        [ProducesResponseType<RetornoCliente>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Retorna o cliente padrao")]
        public async Task<IActionResult> GetDefaultClient()
            => await CommonExecution("Clientes.GetDefaultClient", clienteService.SelecionarClientePadrao());
    }
}