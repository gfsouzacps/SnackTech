using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Produto;

namespace SnackTech.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<Result<IEnumerable<RetornoProduto>>> BuscarPorCategoria(int categoriaId);
        Task<Result<RetornoProduto>> BuscarProdutoPorIdentificacao(string identificacao);
        Task<Result<Guid>> CriarNovoProduto(NovoProduto novoProduto);
        Task<Result> EditarProduto(Guid identificacao, EdicaoProduto edicaoProduto);
        Task<Result> RemoverProduto(string identificacao);
    }
}