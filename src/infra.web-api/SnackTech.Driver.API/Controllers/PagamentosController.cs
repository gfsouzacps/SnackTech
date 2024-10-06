using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SnackTech.Common.Dto.Api;
using SnackTech.Core.Interfaces;
using SnackTech.Driver.API.CustomResponses;
using Swashbuckle.AspNetCore.Annotations;

namespace SnackTech.Driver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentosController(ILogger<PagamentosController> logger, IPagamentoController pagamentoDomainController) : CustomBaseController(logger)
    {
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("")]
        [SwaggerOperation(Summary = "Rota que ao ser chamada valida o payload e caso seja de um pagamento de pedido, atualiza ele para ser preparado")]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna o identificador do novo pedido")]
        public async Task<IActionResult> PagamentoHook([FromBody] PagamentoDto pagamento)
            => await ExecucaoPadrao("Pagamento.ProcessarHook",pagamentoDomainController.ProcessarPagamento(pagamento));
    }
}