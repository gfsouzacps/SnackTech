using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Produto;
using SnackTech.Application.Interfaces;

namespace SnackTech.Application.UseCases
{
    public class ProdutoService : IProdutoService
    {
        public Task<Result<IEnumerable<RetornoProduto>>> BuscarPorCategoria(int categoriaId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<RetornoProduto>> BuscarProdutoPorIdentificacao(string identificacao)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Guid>> CriarNovoProduto(NovoProduto novoProduto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> EditarProduto(EdicaoProduto edicaoProduto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> RemoverProduto(string identificacao)
        {
            throw new NotImplementedException();
        }
    }
}