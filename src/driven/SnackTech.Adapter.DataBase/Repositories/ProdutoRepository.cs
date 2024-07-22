using SnackTech.Domain.Contracts;
using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        public Task AlterarProduto(Produto produtoAlterado)
        {
            throw new NotImplementedException();
        }

        public Task InserirProduto(Produto novoProduto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Produto>> PesquisarPorCategoria(CategoriaProduto categoria)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> PesquisarPorId(Guid identificacao)
        {
            throw new NotImplementedException();
        }

        public Task RemoverProdutoPorIdentificacao(Guid identificacao)
        {
            throw new NotImplementedException();
        }
    }
}