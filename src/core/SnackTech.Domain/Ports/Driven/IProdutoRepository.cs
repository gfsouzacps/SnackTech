using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;

namespace SnackTech.Domain.Ports.Driven
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> PesquisarPorCategoriaAsync(CategoriaProduto categoria);
        Task<Produto?> PesquisarPorIdentificacaoAsync(Guid identificacao);
        Task InserirProdutoAsync(Produto novoProduto);
        Task AlterarProdutoAsync(Produto produtoAlterado);
        Task<bool> RemoverProdutoPorIdentificacaoAsync(Guid identificacao);
    }
}