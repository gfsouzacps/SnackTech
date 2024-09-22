using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using SnackTech.Driver.API.CustomResponses;
using SnackTech.Domain.DTOs.Driving.Produto;
using Swashbuckle.AspNetCore.Annotations;
using SnackTech.Common.Dto;
using SnackTech.Common.Interfaces.Controllers;


namespace SnackTech.Driver.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController(ILogger<ProdutosController> logger, IProdutoController produtoController) : CustomBaseController(logger)
    {
        
        /// <summary>
        /// Cadastra um novo produto no sistema.
        /// </summary>
        /// <param name="novoProduto">Os dados do novo produto a ser cadastrado.</param>
        /// <returns>Um <see cref="IActionResult"/> contendo o identificador do novo produto ou um erro correspondente.</returns>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType<Guid>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Cadastra um novo produto no sistema")]
        public async Task<IActionResult> Post([FromBody] ProdutoSemIdDto novoProduto)
            => await ExecucaoPadrao("Produtos.Post", produtoController.CadastrarNovoProduto(novoProduto));

        /// <summary>
        /// Edita um produto existente no sistema.
        /// </summary>
        /// <param name="identificacao">O guid do produto a ser editado.</param>
        /// <param name="produtoEditado">Os novos dados do produto.</param>
        /// <returns>Um <see cref="IActionResult"/> indicando o sucesso ou falha da opera��o.</returns>
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{identificacao:guid}")]
        [SwaggerOperation(Summary = "Edita um produto existente no sistema")]
        public async Task<IActionResult> Put([FromRoute] Guid identificacao, [FromBody] ProdutoSemIdDto produtoEditado)
            => await ExecucaoPadrao("Produtos.Put", produtoController.EditarProduto(identificacao, produtoEditado));

        /// <summary>
        /// Remove um produto existente no sistema.
        /// </summary>
        /// <param name="identificacao">O guid do produto a ser removido.</param>
        /// <returns>Um <see cref="IActionResult"/> indicando o sucesso ou falha da opera��o.</returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{identificacao:guid}")]
        [SwaggerOperation(Summary = "Remove um produto existente no sistema")]
        public async Task<IActionResult> Delete([FromRoute] Guid identificacao)
            => await ExecucaoPadrao("Produtos.Delete", produtoController.RemoverProduto(identificacao));

        /// <summary>
        /// Retorna todos os produtos de uma categoria informada.
        /// </summary>
        /// <param name="categoriaId">O identificador da categoria (1. Lanche, 2. Acompanhamento, 3. Bebida, 4. Sobremesa).</param>
        /// <returns>Um <see cref="IActionResult"/> contendo a lista de produtos da categoria ou um erro correspondente.</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<RetornoProduto>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status500InternalServerError)]
        [Route("{categoriaId:int}")]
        [SwaggerOperation(Summary = "Retorna todos os produtos de uma categoria informada. Categorias cadastradas: 1. Lanche, 2. Acompanhamento, 3. Bebida, 4. Sobremesa")]
        public async Task<IActionResult> GetByCategory(int categoriaId)
            => await ExecucaoPadrao("Produtos.GetPorCategoria", produtoController.BuscarProdutosPorCategoria(categoriaId));
    }
}