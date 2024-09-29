using Microsoft.AspNetCore.Mvc;
using SnackTech.Common.Dto.Api;
using SnackTech.Core.Interfaces;
using SnackTech.Driver.API.CustomResponses;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace SnackTech.Driver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController(ILogger<ClientesController> logger, IClienteController clienteDomainController) : CustomBaseController(logger)
    {
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
        [ProducesResponseType<ClienteDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo cliente no sistema")]
        public async Task<IActionResult> Post([FromBody] ClienteSemIdDto cadastroCliente)
            => await ExecucaoPadrao("Clientes.Post", clienteDomainController.CadastrarNovoCliente(cadastroCliente));

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
        [ProducesResponseType<ClienteDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Retorna o cliente com o CPF informado")]
        public async Task<IActionResult> GetByCpf([FromRoute] string cpf)
            => await ExecucaoPadrao("Clientes.GetByCpf", clienteDomainController.IdentificarPorCpf(cpf));

        /// <summary>
        /// Retorna o cliente padrao.
        /// </summary>
        /// <remarks>
        /// Retorna as informa��es do cliente padr?o do sistema.
        /// </remarks>
        /// <returns>Um <see cref="IActionResult"/> contendo o cliente encontrado ou um erro correspondente.</returns>
        [HttpGet]
        [Route("cliente-padrao")]
        [ProducesResponseType<ClienteDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Retorna o cliente padrao")]
        public async Task<IActionResult> GetDefaultClient()
            => await ExecucaoPadrao("Clientes.GetDefaultClient", clienteDomainController.SelecionarClientePadrao());
    }
}