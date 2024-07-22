using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;

namespace SnackTech.Domain.Contracts
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> PesquisarPorCategoria(CategoriaProduto categoria);
        Task<Produto?> PesquisarPorId(Guid identificacao);
        Task InserirProduto(Produto novoProduto);
        Task AlterarProduto(Produto produtoAlterado);
        Task RemoverProdutoPorIdentificacao(Guid identificacao);
    }
}