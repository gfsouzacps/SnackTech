using SnackTech.Common.Dto;
using SnackTech.Common.Interface;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Gateways
{
    internal class ProdutoGateway(IProdutoDataSource dataSource)
    {   
        internal async Task<Produto?> ProcurarProdutoPorNome(StringNaoVaziaOuComEspacos nome){
            var produtoDto = await dataSource.ProcurarPorNome(nome.ToString());

            if(produtoDto == null){
                return null;
            }

            return new Produto(produtoDto);
        }     

        internal async Task<Produto?> ProcurarProdutoPorIdentificacao(Guid id){
            var produtoDto = await dataSource.ProcurarPorGuid(id);

            if(produtoDto == null){
                return null;
            }

            return new Produto(produtoDto);
        }

        internal async Task CadastrarNovoProduto(Produto novoProduto){
            var produtoDto = new ProdutoDto(){
                Id = novoProduto.Id,
                Categoria = novoProduto.Categoria.Valor,
                Nome = novoProduto.Nome.ToString(),
                Descricao = novoProduto.Descricao.ToString(),
                Valor = novoProduto.Valor.Valor
            };

            await dataSource.Inserir(produtoDto);
        }

        internal async Task AtualizarProduto(Produto produtoAlterado){
            var produtoDto = new ProdutoDto(){
                Id = produtoAlterado.Id,
                Categoria = produtoAlterado.Categoria.Valor,
                Nome = produtoAlterado.Nome.ToString(),
                Descricao = produtoAlterado.Descricao.ToString(),
                Valor = produtoAlterado.Valor.Valor
            };

            await dataSource.Atualizar(produtoDto);
        }

        internal async Task RemoverProduto(Produto produto){
            await dataSource.RemoverPorGuid(produto.Id);
        }

        internal async Task<IEnumerable<Produto>> ProcurarProdutosPorCategoria(CategoriaProdutoValido categoriaProduto){
            var listaProdutos = await dataSource.ListarPorCategoria(categoriaProduto.Valor);

            return listaProdutos.Select(produtoDto => new Produto(produtoDto));
        }
    }
}