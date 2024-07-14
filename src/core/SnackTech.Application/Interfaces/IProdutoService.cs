using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Produto;

namespace SnackTech.Application.Interfaces
{
    public interface IProdutoService
    {
        /*
            Criar produto novo
            Editar produto
            Remover produto
            Buscar produtos por categoria
                Validar se categoria existe
                Buscar produtos
            Buscar produto por Guid
        */
        Task<Result<IEnumerable<RetornoProduto>>> BuscarPorCategoria(int categoriaId);
        Task<Result<RetornoProduto>> BuscarProdutoPorIdentificacao(string identificacao);
        Task<Result<Guid>> CriarNovoProduto(NovoProduto novoProduto);
        Task<Result> EditarProduto(EdicaoProduto edicaoProduto);
        Task<Result> RemoverProduto(string identificacao);
    }
}