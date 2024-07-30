using Microsoft.Extensions.Logging;
using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Produto;
using SnackTech.Application.Interfaces;
using SnackTech.Domain.Contracts;
using SnackTech.Domain.Guards;
using SnackTech.Domain.Models;

namespace SnackTech.Application.UseCases
{
    public class ProdutoService(ILogger<ProdutoService> logger, IProdutoRepository produtoRepository) : BaseService(logger),IProdutoService
    {
        private readonly IProdutoRepository produtoRepository = produtoRepository;

        public async Task<Result<IEnumerable<RetornoProduto>>> BuscarPorCategoria(int categoriaId)
        {
            async Task<Result<IEnumerable<RetornoProduto>>> processo()
            {
                var categoriaProduto = CustomGuards.AgainstInvalidCategoriaProduto(categoriaId, nameof(categoriaId));
                var produtos = await produtoRepository.PesquisarPorCategoriaAsync(categoriaProduto);
                var retorno = produtos.Select(RetornoProduto.APartirDeProduto);
                return new Result<IEnumerable<RetornoProduto>>(retorno);
            }
            return await CommonExecution($"ProdutoService.BuscarPorCategoria {categoriaId}",processo);
        }

        public async Task<Result<RetornoProduto>> BuscarProdutoPorIdentificacao(string identificacao)
        {
            async Task<Result<RetornoProduto>> processo(){
                var guid = CustomGuards.AgainstInvalidGuid(identificacao,nameof(identificacao));
                var produto = await produtoRepository.PesquisarPorIdentificacaoAsync(guid);

                if(produto is null)
                    return new Result<RetornoProduto>($"Produto com identificação {identificacao} não encontrado.",true);

                var retorno = RetornoProduto.APartirDeProduto(produto);
                return new Result<RetornoProduto>(retorno);
            }
            return await CommonExecution($"ProdutoService.BuscarProdutoPorIdentificacao {identificacao}",processo);
        }

        public async Task<Result<Guid>> CriarNovoProduto(NovoProduto novoProduto)
        {
            async Task<Result<Guid>> processo(){
                var categoriaProduto = CustomGuards.AgainstInvalidCategoriaProduto(novoProduto.Categoria,nameof(novoProduto.Categoria));
                var produto = new Produto(categoriaProduto,novoProduto.Nome,novoProduto.Descricao,novoProduto.Valor);
                await produtoRepository.InserirProdutoAsync(produto);
                return new Result<Guid>(produto.Id);
            }
            return await CommonExecution($"ProdutoService.CriarNovoProduto",processo);
        }

        public async Task<Result> EditarProduto(Guid identificacao, EdicaoProduto edicaoProduto)
        {
            async Task<Result> processo(){
                var categoriaProduto = CustomGuards.AgainstInvalidCategoriaProduto(edicaoProduto.Categoria,nameof(edicaoProduto.Categoria));

                var produtoDaBase = await produtoRepository.PesquisarPorIdentificacaoAsync(identificacao);
                if(produtoDaBase is null)
                    return new Result($"Produto com identificação {identificacao} não encontrado na base.");

                produtoDaBase.AtualizarDadosProduto(categoriaProduto,edicaoProduto.Nome,edicaoProduto.Descricao,edicaoProduto.Valor);

                await produtoRepository.AlterarProdutoAsync(produtoDaBase);
                return new Result();
            }
            return await CommonExecution($"ProdutoService.EditarProduto {identificacao}",processo);
        }

        public async Task<Result> RemoverProduto(string identificacao)
        {
            async Task<Result> processo(){
                var guid = CustomGuards.AgainstInvalidGuid(identificacao,nameof(identificacao));

                var success = await produtoRepository.RemoverProdutoPorIdentificacaoAsync(guid);

                if (success)
                    return new Result();
                else
                    return new Result($"Produto com identificação {identificacao} não encontrado.");
            }
            return await CommonExecution($"ProdutoService.RemoverProduto {identificacao}", processo);
        }
    }
}