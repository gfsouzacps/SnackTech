using SnackTech.Common.Dto;
using SnackTech.Common.Interfaces;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Gateways
{
    internal class ProdutoGateway(IProdutoDataSource dataSource)
    {       
        internal async Task<Produto?> ProcurarProdutoPorNome(StringNaoVaziaOuComEspacos nome){
            var produtoDto = await dataSource.PesquisarPorNomeAsync(nome.ToString());

            if(produtoDto == null){
                return null;
            }

            return new Produto(produtoDto);
        }     

        internal async Task<Produto?> ProcurarProdutoPorIdentificacao(Guid id){
            var produtoDto = await dataSource.PesquisarPorIdentificacaoAsync(id);

            if(produtoDto == null){
                return null;
            }

            return new Produto(produtoDto);
        }

        internal async Task<bool> CadastrarNovoProduto(Produto novoProduto){            
            ProdutoDto dto = novoProduto;

            return await dataSource.InserirProdutoAsync(dto);
        }

        internal async Task<bool> AtualizarProduto(Produto produtoAlterado){
            ProdutoDto dto = produtoAlterado;

            return await dataSource.AlterarProdutoAsync(dto);
        }

        internal async Task<bool> RemoverProduto(Guid id){
            return await dataSource.RemoverProdutoPorIdentificacaoAsync(id);
        }

        internal async Task<IEnumerable<Produto>> ProcurarProdutosPorCategoria(CategoriaProdutoValido categoriaProduto){
            var produtosDto = await dataSource.PesquisarPorCategoriaIdAsync(categoriaProduto);

            return produtosDto.Select(p => new Produto(p));
        }
    }
}