using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using SnackTech.Core.Gateways;

namespace SnackTech.Core.UseCases
{
    internal static class ProdutoUseCase
    {
        internal static async Task<ResultadoOperacao<Produto>> CriarNovoProduto(ProdutoDto produtoDto, ProdutoGateway produtoGateway){
            try{
                //garantir que não existe produto com mesmo nome já cadastrado
                var produto = await produtoGateway.ProcurarProdutoPorNome(produtoDto.Nome);

                if(produto == null){
                    return new ResultadoOperacao<Produto>($"Produto {produtoDto.Nome} já cadastrado.", true);
                }

                //chamar gateway que fala com a fonte de dados para cadastrar produto
                var entidade = new Produto(produtoDto.Id,
                                           produtoDto.Categoria,
                                           produtoDto.Nome,
                                           produtoDto.Descricao,
                                           produtoDto.Valor);

                var novoProduto = await produtoGateway.CadastrarNovoProduto(entidade);
                //retorna entidade
                return new ResultadoOperacao<Produto>(novoProduto);
            }
            catch(Exception ex){
                return new ResultadoOperacao<Produto>(ex);
            }
        }

        internal static async Task<ResultadoOperacao> EditarProduto(ProdutoDto produtoDto, ProdutoGateway produtoGateway){
            try{
                //chamar gateway que fala com a fonte de dados para resgatar entidade produto
                var produto = await produtoGateway.ProcurarProdutoPorIdentificacao(produtoDto.Id);

                //se não existir retornar que não pode atualizar produto que não existe
                if(produto == null){
                    return new ResultadoOperacao($"Produto {produtoDto.Id} - {produtoDto.Nome} não encontrado. Atualização não executada!");
                }
                //se existe alterar entidade, repassar para gateway
                produto.AlterarDados(produtoDto);
                                           
                await produtoGateway.AtualizarProduto(produto);
                //retorna operacao com sucesso
                return new ResultadoOperacao();
            }
            catch(Exception ex){
                return new ResultadoOperacao(ex);
            }
        }

        internal static async Task<ResultadoOperacao> RemoverProduto(Guid identificacao, ProdutoGateway produtoGateway){
            try{
                //chamar gateway que fala com a fonte de dados para resgatar entidade produto
                var produto = await produtoGateway.ProcurarProdutoPorIdentificacao(identificacao);
                //se não existir retornar que não pode remover produto que não existe
                if(produto == null){
                    return new ResultadoOperacao($"Produto {identificacao} não encontrado. Remoção não executada!");
                }
                //se existe repassar para gateway para que remova o produto
                await produtoGateway.RemoverProduto(identificacao);
                //retorna sucesso da execucao
                return new ResultadoOperacao();
            }
            catch(Exception ex){
                return new ResultadoOperacao(ex);
            }
        }

        internal static async Task<ResultadoOperacao<IEnumerable<Produto>>> BuscarProdutoPorCategoria(int categoriaProduto, ProdutoGateway produtoGateway){
            try{
                var categoriaValida = new CategoriaProdutoValido(categoriaProduto);
                var listaProdutos = await produtoGateway.ProcurarProdutosPorCategoria(categoriaValida);
                return new ResultadoOperacao<IEnumerable<Produto>>(listaProdutos);
            }
            catch(ArgumentException ex){
                return new ResultadoOperacao<IEnumerable<Produto>>($"Erro de Argumento: {ex.Message}",true);
            }
            catch(Exception ex){
                return new ResultadoOperacao<IEnumerable<Produto>>(ex);
            }
        }

        internal static async Task<ResultadoOperacao<Produto>> BuscarProdutoPorIdentificacao(Guid identificacao, ProdutoGateway produtoGateway){
            try{
                var produto = await produtoGateway.ProcurarProdutoPorIdentificacao(identificacao);

                if(produto == null){
                    return new ResultadoOperacao<Produto>($"Produto {identificacao} não encontrado.", true);
                }

                return new ResultadoOperacao<Produto>(produto);
            }
            catch(Exception ex){
                return new ResultadoOperacao<Produto>(ex);
            }
        }
    }
}