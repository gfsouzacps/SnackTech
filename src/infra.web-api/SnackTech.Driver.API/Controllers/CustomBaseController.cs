using Microsoft.AspNetCore.Mvc;
using SnackTech.Driver.API.CustomResponses;
using SnackTech.Domain.Common;
using System.Runtime.CompilerServices;
using SnackTech.Common.Dto;

[assembly: InternalsVisibleTo("SnackTech.Driver.API.Tests")]
namespace SnackTech.Driver.API.Controllers
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
                logger.LogError(ex, "{@Template}", $"{nomeMetodo} - Exception - {ex.Message}");
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

        internal async Task<IActionResult> ExecucaoPadrao<T>(string nomeMetodo, Task<ResultadoOperacao<T>> processo)
        {
            try
            {
                var resultado = await processo;

                if (resultado.TeveSucesso())
                    return Ok(resultado.Dados);

                if (resultado.TeveExcecao())
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(resultado.Mensagem, new ExceptionResponse(resultado.Excecao)));

                var errorResponse = new ErrorResponse(resultado.Mensagem, null);
                return BadRequest(errorResponse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Metodo} - Exception - {Message}", nomeMetodo, ex.Message);
                var retorno = new ErrorResponse(ex.Message, new ExceptionResponse(ex));
                return StatusCode(StatusCodes.Status500InternalServerError, retorno);
            }
        }

        internal async Task<IActionResult> ExecucaoPadrao(string nomeMetodo, Task<ResultadoOperacao> processo)
        {
            try
            {
                var resultado = await processo;

                if (resultado.TeveSucesso())
                    return Ok();

                if (resultado.TeveExcecao())
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(resultado.Mensagem, new ExceptionResponse(resultado.Excecao)));

                var errorResponse = new ErrorResponse(resultado.Mensagem, null);
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
