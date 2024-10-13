using SnackTech.Common.Dto.Api;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters;

internal static class ProdutoPresenter
{
    internal static ResultadoOperacao<ProdutoDto> ApresentarResultadoProduto(Produto produto){
        ProdutoDto produtoDto = ConverterParaDto(produto);
        return new ResultadoOperacao<ProdutoDto>(produtoDto);
    }

    internal static ResultadoOperacao<IEnumerable<ProdutoDto>> ApresentarResultadoListaProdutos(IEnumerable<Produto> produtos){
        var produtosDtos = produtos.Select(ConverterParaDto);
        return new ResultadoOperacao<IEnumerable<ProdutoDto>>(produtosDtos);
    }

    internal static ProdutoDto ConverterParaDto(Produto produto)
    {
        return new ProdutoDto
        {
            IdentificacaoProduto = produto.Id,
            Categoria = produto.Categoria,
            Descricao = produto.Descricao,
            Nome = produto.Nome,
            Valor = produto.Valor
        };
    }
}