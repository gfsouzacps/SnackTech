using SnackTech.Domain.Common;
using SnackTech.Domain.DTOs.Produto;

namespace SnackTech.Domain.Ports.Driving
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