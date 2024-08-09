using Microsoft.AspNetCore.Mvc;
using SnackTech.API.CustomResponses;
using SnackTech.Domain.Common;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SnackTech.API.Tests")]
namespace SnackTech.API.Controllers
{
    public abstract class CustomBaseController(ILogger logger) : ControllerBase
    {
        private readonly ILogger logger = logger;

        internal async Task<IActionResult> CommonExecution<T>(string nomeMetodo, Task<Result<T>> processo)
        {
            try
            {
                var resultado = await processo;

                if (resultado.IsSuccess())
                    return Ok(resultado.Data);

                if (resultado.HasException())
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(resultado.Message, new ExceptionResponse(resultado.Exception)));

                var errorResponse = new ErrorResponse(resultado.Message, null);
                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Metodo} - Exception - {Message}", nomeMetodo, ex.Message);
                var retorno = new ErrorResponse(ex.Message, new ExceptionResponse(ex));
                return StatusCode(StatusCodes.Status500InternalServerError, retorno);
            }
        }

        internal async Task<IActionResult> CommonExecution(string nomeMetodo, Task<Result> processo)
        {
            try
            {
                var resultado = await processo;

                if (resultado.IsSuccess())
                    return Ok();

                if (resultado.HasException())
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(resultado.Message, new ExceptionResponse(resultado.Exception)));

                var errorResponse = new ErrorResponse(resultado.Message, null);
                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Metodo} - Exception - {Message}", nomeMetodo, ex.Message);
                var retorno = new ErrorResponse(ex.Message, new ExceptionResponse(ex));
                return StatusCode(StatusCodes.Status500InternalServerError, retorno);
            }
        }
    }
}
