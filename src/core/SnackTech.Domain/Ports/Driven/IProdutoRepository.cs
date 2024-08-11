using SnackTech.Domain.Enums;

namespace SnackTech.Domain.Ports.Driven
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<DTOs.Driven.ProdutoDto>> PesquisarPorCategoriaAsync(CategoriaProduto categoria);
        Task<DTOs.Driven.ProdutoDto?> PesquisarPorIdentificacaoAsync(Guid identificacao);
        Task InserirProdutoAsync(DTOs.Driven.ProdutoDto novoProduto);
        Task AlterarProdutoAsync(DTOs.Driven.ProdutoDto produtoAlterado);
        Task<bool> RemoverProdutoPorIdentificacaoAsync(Guid identificacao);
    }
}