using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Gateways
{
    internal class ProdutoGateway
    {
        //Plugar com a persistência
        //No nosso caso, com o DbContext?
        
        internal Task<Produto> ProcurarProdutoPorNome(StringNaoVaziaOuComEspacos nome){
            throw new NotImplementedException();
        }     

        internal Task<Produto> ProcurarProdutoPorIdentificacao(Guid id){
            throw new NotImplementedException();
        }

        internal Task<Produto> CadastrarNovoProduto(Produto novoProduto){            
            throw new NotImplementedException();
        }

        internal Task AtualizarProduto(Produto produtoAlterado){
            throw new NotImplementedException();
        }

        internal Task RemoverProduto(Guid id){
            throw new NotImplementedException();
        }

        internal Task<IEnumerable<Produto>> ProcurarProdutosPorCategoria(CategoriaProdutoValido categoriaProduto){
            throw new NotImplementedException();
        }
    }
}