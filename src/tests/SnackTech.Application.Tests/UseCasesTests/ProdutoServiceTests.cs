using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Application.DTOs.Produto;
using SnackTech.Application.UseCases;
using SnackTech.Domain.Contracts;
using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;

namespace SnackTech.Application.Tests.UseCasesTests
{
    public class ProdutoServiceTests
    {
        private readonly Mock<ILogger<ProdutoService>> logger;
        private readonly Mock<IProdutoRepository> produtoRepository;
        private readonly ProdutoService produtoService;

        public ProdutoServiceTests(){
            logger = new Mock<ILogger<ProdutoService>>();
            produtoRepository = new Mock<IProdutoRepository>();
            produtoService = new ProdutoService(logger.Object,produtoRepository.Object);
        }

        [Fact]
        public async Task BuscarPorCategoriaWithSuccessAndObjects(){
            produtoRepository.Setup(p => p.PesquisarPorCategoria(It.IsAny<CategoriaProduto>()))
                                .ReturnsAsync(new List<Produto>{
                                    new Produto(CategoriaProduto.Lanche,"Lanche A","descricao",10),
                                    new Produto(CategoriaProduto.Lanche,"Lanche B","descricao",25),
                                    new Produto(CategoriaProduto.Lanche,"Lanche C","descricao",30),
                                    new Produto(CategoriaProduto.Lanche,"Lanche D","descricao",20)
                                });

            var resultado = await produtoService.BuscarPorCategoria(1);

            Assert.True(resultado.IsSuccess());
            Assert.True(resultado.Data.Any());
            Assert.Equal(4,resultado.Data.Count());
        }

        [Fact]
        public async Task BuscarPorCategoriaWithSuccessButNoObjects(){
            produtoRepository.Setup(p => p.PesquisarPorCategoria(It.IsAny<CategoriaProduto>()))
                                .ReturnsAsync(Array.Empty<Produto>());

            var resultado = await produtoService.BuscarPorCategoria(1);

            Assert.True(resultado.IsSuccess());
            Assert.True(!resultado.Data.Any());
            Assert.Equal(0,resultado.Data.Count());
        }

        [Fact]
        public async Task BuscarPorCategoriaWithInvalidCategory(){
            var resultado = await produtoService.BuscarPorCategoria(10);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.Contains("categoriaId não é um CategoriaProduto válido.",resultado.Message);
        }

        [Fact]
        public async Task BuscarPorCategoriaWithException(){
            produtoRepository.Setup(p => p.PesquisarPorCategoria(It.IsAny<CategoriaProduto>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));
            var resultado = await produtoService.BuscarPorCategoria(1);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains("Erro inesperado",resultado.Exception.Message);
        }

        [Fact]
        public async Task BuscarProdutoPorIdentificacaoWithSuccess(){
            var identificacao = Guid.NewGuid();
            var identificacaoString = identificacao.ToString();
            produtoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(new Produto(identificacao,CategoriaProduto.Lanche,"Lanche A","descricao",10));

            var resultado = await produtoService.BuscarProdutoPorIdentificacao(identificacaoString);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.GetValue());
            Assert.Equal(identificacaoString,resultado.GetValue().Identificacao);
        }

        [Fact]
        public async Task BuscarProdutoPorIdentificacaoInvalidGuid(){
            var identificacao = "";

            var resultado = await produtoService.BuscarProdutoPorIdentificacao(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.Message);
            Assert.Contains("não é um Guid válido.",resultado.Message);
        }

        [Fact]
        public async Task BuscarProdutoPorIdentificacaoNotFounded(){
            var identificacao = Guid.NewGuid().ToString();
            produtoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(default(Produto));

            var resultado = await produtoService.BuscarProdutoPorIdentificacao(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.Message);
            Assert.Contains($"Produto com identificação {identificacao} não encontrado.",resultado.Message);
        }

        [Fact]
        public async Task BuscarProdutoPorIdentificacaoException(){
            var identificacao = Guid.NewGuid().ToString();
            produtoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await produtoService.BuscarProdutoPorIdentificacao(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains($"Erro inesperado",resultado.Message);
        }

        [Fact]
        public async Task CriarNovoProdutoWithSuccess(){
            var novoProduto = new NovoProduto{
                Categoria = 1,
                Descricao = "descricao",
                Nome = "Produto",
                Valor = 15
            };

            produtoRepository.Setup(p => p.InserirProduto(It.IsAny<Produto>()))
                                .Returns(Task.FromResult(0));
            
            var resultado = await produtoService.CriarNovoProduto(novoProduto);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
        }

        [Fact]
        public async Task CriarNovoProdutoWithArgumentException(){
            var novoProduto = new NovoProduto{
                Categoria = 15,
                Descricao = "descricao",
                Nome = "Produto",
                Valor = 15
            };

             var resultado = await produtoService.CriarNovoProduto(novoProduto);

             Assert.False(resultado.IsSuccess());
             Assert.Null(resultado.Exception);
             Assert.Contains("não é um CategoriaProduto válido.",resultado.Message);
        }

        [Fact]
        public async Task CriarNovoProdutoWithException(){
            var novoProduto = new NovoProduto{
                Categoria = 2,
                Descricao = "descricao",
                Nome = "Produto",
                Valor = 20
            };

            produtoRepository.Setup(p => p.InserirProduto(It.IsAny<Produto>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

             var resultado = await produtoService.CriarNovoProduto(novoProduto);

             Assert.False(resultado.IsSuccess());
             Assert.NotNull(resultado.Exception);
             Assert.Contains("Erro inesperado",resultado.Message);
        }

        [Fact]
        public async Task EditarProdutoWithSuccess(){
            var identificacao = Guid.NewGuid();
            var produtoEditado = new EdicaoProduto{
                Categoria = 2,
                Descricao = "descricao a",
                Identificacao = identificacao.ToString(),
                Nome = "Nome",
                Valor = 10
            };

            produtoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(new Produto(CategoriaProduto.Acompanhamento,"Nome","descricao", 12));
            produtoRepository.Setup(p => p.AlterarProduto(It.IsAny<Produto>()))
                            .Returns(Task.FromResult(0));

            var resultado = await produtoService.EditarProduto(produtoEditado);

            Assert.True(resultado.IsSuccess());
        }

        [Fact]
        public async Task EditarProdutoButProductNotFounded(){
            var identificacao = Guid.NewGuid();
            var produtoEditado = new EdicaoProduto{
                Categoria = 2,
                Descricao = "descricao a",
                Identificacao = identificacao.ToString(),
                Nome = "Nome",
                Valor = 10
            };

            produtoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(default(Produto));

            var resultado = await produtoService.EditarProduto(produtoEditado);

             Assert.False(resultado.IsSuccess());
             Assert.Null(resultado.Exception);
             Assert.Contains($"Produto com identificação {identificacao} não encontrado na base.",resultado.Message);
        }

        [Fact]
        public async Task EditarProdutoWithException(){
            var identificacao = Guid.NewGuid();
            var produtoEditado = new EdicaoProduto{
                Categoria = 2,
                Descricao = "descricao a",
                Identificacao = identificacao.ToString(),
                Nome = "Nome",
                Valor = 10
            };

            produtoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(new Produto(CategoriaProduto.Acompanhamento,"Nome","descricao", 12));
            produtoRepository.Setup(p => p.AlterarProduto(It.IsAny<Produto>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await produtoService.EditarProduto(produtoEditado);

             Assert.False(resultado.IsSuccess());
             Assert.NotNull(resultado.Exception);
             Assert.Contains("Erro inesperado",resultado.Exception.Message);
        }

        [Fact]
        public async Task RemoverProdutoWithSuccess(){
            var identificacao = Guid.NewGuid();
            produtoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(new Produto(CategoriaProduto.Acompanhamento,"Nome","descricao", 12));
            produtoRepository.Setup(p => p.RemoverProdutoPorIdentificacao(It.IsAny<Guid>()))
                            .Returns(Task.FromResult(0));
            var resultado = await produtoService.RemoverProduto(identificacao.ToString());

            Assert.True(resultado.IsSuccess());
        }

        [Fact]
        public async Task RemoverProdutoButProductNotFounded(){
            var identificacao = Guid.NewGuid();
            produtoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(default(Produto));

            var resultado = await produtoService.RemoverProduto(identificacao.ToString());

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.Contains($"Produto com identificação {identificacao} não encontrado.",resultado.Message);
        }

        [Fact]
        public async Task RemoverProdutoWithException(){
            var identificacao = Guid.NewGuid();
            produtoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(new Produto(CategoriaProduto.Acompanhamento,"Nome","descricao", 12));
            produtoRepository.Setup(p => p.RemoverProdutoPorIdentificacao(It.IsAny<Guid>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await produtoService.RemoverProduto(identificacao.ToString());
            
            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains("Erro inesperado",resultado.Exception.Message);
        }
    }
}