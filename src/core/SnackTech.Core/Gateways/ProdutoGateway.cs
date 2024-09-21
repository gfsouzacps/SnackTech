using SnackTech.Core.Common.Dto;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Gateways
{
    public class ProdutoGateway
    {
        //Plugar com a persistência
        //No nosso caso, com o DbContext?
        
        public Task<Produto> ProcurarProdutoPorNome(StringNaoVaziaOuComEspacos nome){
            throw new NotImplementedException();
        }     

        public Task<Produto> ProcurarProdutoPorIdentificacao(Guid id){
            throw new NotImplementedException();
        }

        public Task<Produto> CadastrarNovoProduto(Produto novoProduto){            
            throw new NotImplementedException();
        }

        public Task AtualizarProduto(Produto produtoAlterado){
            throw new NotImplementedException();
        }

        public Task RemoverProduto(Guid id){
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Produto>> ProcurarProdutosPorCategoria(CategoriaProdutoValido categoriaProduto){
            throw new NotImplementedException();
        }
    }
}