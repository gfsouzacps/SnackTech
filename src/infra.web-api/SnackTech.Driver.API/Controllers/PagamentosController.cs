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
        
        /*
        Exemplo de payload que o MP mandaria pra essa rota:
        {
            "action": "update",
            "application_id": "7910800073137785",
            "data": {
                "currency_id": "",
                "marketplace": "NONE",
                "status": "closed"
            },
            "date_created": "2024-10-06T17:42:13.510-04:00",
            "id": "23603350837",
            "live_mode": false,
            "status": "closed",
            "type": "topic_merchant_order_wh",
            "user_id": 2012037660,
            "version": 1
        }

        Nosso processo só fará algo se action = update e status = closed.
        Mercado Pago pode enviar o mesmo payload mas com action = create e status opened
        */
    }
}