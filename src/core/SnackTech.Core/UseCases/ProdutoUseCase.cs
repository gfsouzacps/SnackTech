using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using SnackTech.Core.Gateways;
using SnackTech.Core.Presenters;

namespace SnackTech.Core.UseCases
{
    internal static class ProdutoUseCase
    {
        internal static async Task<ResultadoOperacao<ProdutoDto>> CriarNovoProduto(ProdutoSemIdDto produtoDto, ProdutoGateway produtoGateway){
            try{
                //garantir que não existe produto com mesmo nome já cadastrado
                var produto = await produtoGateway.ProcurarProdutoPorNome(produtoDto.Nome);

                if(produto == null){
                    return GeralPresenter.ApresentarResultadoErroLogico<ProdutoDto>($"Produto {produtoDto.Nome} já cadastrado.");
                }

                //chamar gateway que fala com a fonte de dados para cadastrar produto 
                var entidade = new Produto(Guid.NewGuid(),
                                           produtoDto.Categoria,
                                           produtoDto.Nome,
                                           produtoDto.Descricao,
                                           produtoDto.Valor);

                var novoProduto = await produtoGateway.CadastrarNovoProduto(entidade);
                var retorno = novoProduto ? 
                                ProdutoPresenter.ApresentarResultadoProduto(entidade):
                                GeralPresenter.ApresentarResultadoErroLogico<ProdutoDto>($"Não foi possível cadastrar produto {entidade.Nome}.");
                
                return retorno;
            }
            catch(Exception ex){
                return GeralPresenter.ApresentarResultadoErroInterno<ProdutoDto>(ex);
            }
        }

        internal static async Task<ResultadoOperacao<ProdutoDto>> EditarProduto(ProdutoDto produtoDto, ProdutoGateway produtoGateway){
            try{
                //chamar gateway que fala com a fonte de dados para resgatar entidade produto
                var produto = await produtoGateway.ProcurarProdutoPorIdentificacao(produtoDto.Id);

                //se não existir retornar que não pode atualizar produto que não existe
                if(produto == null){
                    return GeralPresenter.ApresentarResultadoErroLogico<ProdutoDto>($"Produto {produtoDto.Id} - {produtoDto.Nome} não encontrado. Atualização não executada!");
                }
                //se existe alterar entidade, repassar para gateway
                produto.AlterarDados(produtoDto);
                                           
                await produtoGateway.AtualizarProduto(produto);
                //retorna operacao com sucesso
                return ProdutoPresenter.ApresentarResultadoProduto(produto);
            }
            catch(Exception ex){
                return GeralPresenter.ApresentarResultadoErroInterno<ProdutoDto>(ex);
            }
        }

        internal static async Task<ResultadoOperacao> RemoverProduto(Guid identificacao, ProdutoGateway produtoGateway){
            try{
                //chamar gateway que fala com a fonte de dados para resgatar entidade produto
                var produto = await produtoGateway.ProcurarProdutoPorIdentificacao(identificacao);
                //se não existir retornar que não pode remover produto que não existe
                if(produto == null){
                    return GeralPresenter.ApresentarResultadoErroLogico($"Produto {identificacao} não encontrado. Remoção não executada!");
                }
                //se existe repassar para gateway para que remova o produto
                await produtoGateway.RemoverProduto(identificacao);
                //retorna sucesso da execucao
                return GeralPresenter.ApresentarResultadoPadraoSucesso();
            }
            catch(Exception ex){
                return GeralPresenter.ApresentarResultadoErroInterno(ex);
            }
        }

        internal static async Task<ResultadoOperacao<IEnumerable<ProdutoDto>>> BuscarProdutoPorCategoria(int categoriaProduto, ProdutoGateway produtoGateway){
            try{
                var categoriaValida = new CategoriaProdutoValido(categoriaProduto);
                var listaProdutos = await produtoGateway.ProcurarProdutosPorCategoria(categoriaValida);
                return ProdutoPresenter.ApresentarResultadoListaProdutos(listaProdutos);
            }
            catch(ArgumentException ex){
                return GeralPresenter.ApresentarResultadoErroLogico<IEnumerable<ProdutoDto>>($"Erro de Argumento: {ex.Message}");
            }
            catch(Exception ex){
                return GeralPresenter.ApresentarResultadoErroInterno<IEnumerable<ProdutoDto>>(ex);
            }
        }

        internal static async Task<ResultadoOperacao<ProdutoDto>> BuscarProdutoPorIdentificacao(Guid identificacao, ProdutoGateway produtoGateway){
            try{
                var produto = await produtoGateway.ProcurarProdutoPorIdentificacao(identificacao);

                if(produto == null){
                    return GeralPresenter.ApresentarResultadoErroLogico<ProdutoDto>($"Produto {identificacao} não encontrado.");
                }

                return ProdutoPresenter.ApresentarResultadoProduto(produto);
            }
            catch(Exception ex){
                return GeralPresenter.ApresentarResultadoErroInterno<ProdutoDto>(ex);
            }
        }
    }
}