using SnackTech.Common.Dto.DataSource;

namespace SnackTech.Common.Interfaces.DataSources
{
    public interface IProdutoDataSource
    {
        Task<IEnumerable<ProdutoDto>> PesquisarPorCategoriaIdAsync(int categoriaId);
        Task<ProdutoDto?> PesquisarPorIdentificacaoAsync(Guid identificacao);
        Task<ProdutoDto?> PesquisarPorNomeAsync(string nome);
        Task<bool> InserirProdutoAsync(ProdutoDto produtoNovo);
        Task<bool> AlterarProdutoAsync(ProdutoDto produtoAlterado);
        Task<bool> RemoverProdutoPorIdentificacaoAsync(Guid identificacao);
    }
}