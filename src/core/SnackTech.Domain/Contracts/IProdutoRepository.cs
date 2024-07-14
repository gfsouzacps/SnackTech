using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;

namespace SnackTech.Domain.Contracts
{
    public interface IProdutoRepository
    {
        Task PesquisarPorCategoria(CategoriaProduto categoria);
        Task PesquisarPorId(Guid identificacao);
        Task InserirProduto(Produto novoProduto);
        Task AlterarProduto(Produto produtoAlterado);
        Task RemoverProdutoPorIdentificacao(Guid identificacao);
    }
}