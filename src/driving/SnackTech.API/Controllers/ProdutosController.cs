using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SnackTech.API.CustomResponses;
using SnackTech.Domain.DTOs.Produto;
using SnackTech.Domain.Ports.Driving;
using SnackTech.Domain.Ports.Driven;
using Swashbuckle.AspNetCore.Annotations;

namespace SnackTech.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController(ILogger<ProdutosController> logger, IProdutoService produtoService) : CustomBaseController(logger)
    {
        private readonly IProdutoService produtoService = produtoService;

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo produto no sistema")]
        public async Task<IActionResult> Post([FromBody]NovoProduto novoProduto)
            => await CommonExecution("Produtos.Post",produtoService.CriarNovoProduto(novoProduto));

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{identificacao:guid}")]
        [SwaggerOperation(Summary = "Edita um produto existente no sistema")]
        public async Task<IActionResult> Put([FromRoute]Guid identificacao, [FromBody]EdicaoProduto produtoEditado)
            => await CommonExecution("Produtos.Put",produtoService.EditarProduto(identificacao,produtoEditado));

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{identificacao:guid}")]
        [SwaggerOperation(Summary = "Remove um produto existente no sistema")]
        public async Task<IActionResult> Delete([FromRoute] string identificacao)
            => await CommonExecution("Produtos.Delete",produtoService.RemoverProduto(identificacao));

        [HttpGet]
        [ProducesResponseType<IEnumerable<RetornoProduto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{categoriaId:int}")]
        [SwaggerOperation(Summary = "Retorna todos os produtos de uma categoria informada. Categorias cadastradas: 1. Lanche, 2. Acompanhamento, 3. Bebida, 4. Sobremesa")]
        public async Task<IActionResult> GetByCategory(int categoriaId)
            => await CommonExecution("Produtos.GetPorCategoria",produtoService.BuscarPorCategoria(categoriaId));
    }
}