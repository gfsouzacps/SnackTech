using SnackTech.Common.Dto.Api;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters
{
    internal static class ProdutoPresenter
    {
        internal static ResultadoOperacao<ProdutoDto> ApresentarResultadoProduto(Produto produto){
            ProdutoDto produtoDto = produto;
            return new ResultadoOperacao<ProdutoDto>(produtoDto);
        }

        internal static ResultadoOperacao<IEnumerable<ProdutoDto>> ApresentarResultadoListaProdutos(IEnumerable<Produto> produtos){
            var produtosDtos = produtos.Select(p => {
                ProdutoDto produtoDto = p;
                return produtoDto;
            });
            return new ResultadoOperacao<IEnumerable<ProdutoDto>>(produtosDtos);
        }
    }
}