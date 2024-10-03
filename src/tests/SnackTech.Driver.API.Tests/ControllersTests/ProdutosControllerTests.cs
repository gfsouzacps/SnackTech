//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using SnackTech.Driver.API.Controllers;
//using SnackTech.Driver.API.CustomResponses;
//using SnackTech.Domain.Common;
//using SnackTech.Domain.DTOs.Driving.Produto;
//using SnackTech.Domain.Ports.Driving;

//namespace SnackTech.Driver.API.Tests.ControllersTests
//{
//    public class ProdutosControllerTests
//    {
//        private readonly Mock<ILogger<ProdutosController>> logger;
//        private readonly Mock<IProdutoService> produtoService;
//        private readonly ProdutosController produtosController;
//        public ProdutosControllerTests(){
//            logger = new Mock<ILogger<ProdutosController>>();
//            produtoService = new Mock<IProdutoService>();
//            produtosController = new ProdutosController(logger.Object,produtoService.Object);
//        }

//        [Fact]
//        public async Task PostWithSuccess(){
//            var cadastroProduto = new NovoProduto{};

//            produtoService.Setup(p => p.CriarNovoProduto(It.IsAny<NovoProduto>()))
//                        .ReturnsAsync(new Result<Guid>(Guid.NewGuid()));

//            var resultado = await produtosController.Post(cadastroProduto);

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task PostWithBadRequest(){
//            var cadastroProduto = new NovoProduto{};

//            produtoService.Setup(p => p.CriarNovoProduto(It.IsAny<NovoProduto>()))
//                        .ReturnsAsync(new Result<Guid>("Valor inválido",true));

//            var resultado = await produtosController.Post(cadastroProduto);

//             var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Valor inválido",payload.Message);
//        }

//        [Fact]
//        public async Task PostWithReceivedInternalServerError(){
//            produtoService.Setup(p => p.CriarNovoProduto(It.IsAny<NovoProduto>()))
//                        .ReturnsAsync(new Result<Guid>(new Exception("Erro inesperado")));

//            var resultado = await produtosController.Post(new NovoProduto{});
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }

//        [Fact]
//        public async Task PostWithThrowedInternalServerError(){
//            produtoService.Setup(p => p.CriarNovoProduto(It.IsAny<NovoProduto>()))
//                        .ThrowsAsync(new Exception("Erro inesperado"));

//            var resultado = await produtosController.Post(new NovoProduto{});
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }

//        [Fact]
//        public async Task PutWithSuccess(){
//            var guid = Guid.NewGuid();
//            produtoService.Setup(p => p.EditarProduto(It.IsAny<Guid>(),It.IsAny<EdicaoProduto>()))
//                        .ReturnsAsync(new Result());

//            var resultado = await produtosController.Put(guid,new EdicaoProduto{});

//            Assert.IsType<OkResult>(resultado);
//        }

//        [Fact]
//        public async Task PutWithBadRequest(){
//            var guid = Guid.NewGuid();
//            produtoService.Setup(p => p.EditarProduto(It.IsAny<Guid>(),It.IsAny<EdicaoProduto>()))
//                        .ReturnsAsync(new Result("Valor inválido"));

//            var resultado = await produtosController.Put(guid,new EdicaoProduto{});

//             var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Valor inválido",payload.Message);
//        }

//        [Fact]
//        public async Task PutWithReceivedInternalServerError(){
//            var guid = Guid.NewGuid();
//            produtoService.Setup(p => p.EditarProduto(It.IsAny<Guid>(),It.IsAny<EdicaoProduto>()))
//                        .ReturnsAsync(new Result(new Exception("Erro inesperado")));

//            var resultado = await produtosController.Put(guid,new EdicaoProduto{});
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }

//        [Fact]
//        public async Task PutWithThrowedInternalServerError(){
//            var guid = Guid.NewGuid();
//            produtoService.Setup(p => p.EditarProduto(It.IsAny<Guid>(),It.IsAny<EdicaoProduto>()))
//                        .ThrowsAsync(new Exception("Erro inesperado"));

//            var resultado = await produtosController.Put(guid,new EdicaoProduto{});
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }

//        [Fact]
//        public async Task DeleteWithSuccess(){
//            var identificacao = Guid.NewGuid().ToString();
//            produtoService.Setup(p => p.RemoverProduto(It.IsAny<string>()))
//                            .ReturnsAsync(new Result());

//            var resultado = await produtosController.Delete(identificacao);
//            Assert.IsType<OkResult>(resultado);
//        }

//        [Fact]
//        public async Task DeleteProductDontExist(){
//            var identificacao = Guid.NewGuid().ToString();
//            produtoService.Setup(p => p.RemoverProduto(It.IsAny<string>()))
//                            .ReturnsAsync(new Result($"Produto com identificação {identificacao} não encontrado."));

//            var resultado = await produtosController.Delete(identificacao);
            
//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal($"Produto com identificação {identificacao} não encontrado.",payload.Message);
//        }

//        [Fact]
//        public async Task DeleteWithThrowedException(){
//            var identificacao = Guid.NewGuid().ToString();
//            produtoService.Setup(p => p.RemoverProduto(It.IsAny<string>()))
//                            .ThrowsAsync(new Exception("Erro inesperado"));
            
//            var resultado = await produtosController.Delete(identificacao);
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }

//        [Fact]
//        public async Task DeleteWithReturnedException(){
//            var identificacao = Guid.NewGuid().ToString();
//            produtoService.Setup(p => p.RemoverProduto(It.IsAny<string>()))
//                            .ReturnsAsync(new Result(new Exception("Erro inesperado")));

//            var resultado = await produtosController.Delete(identificacao);

//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }

//        [Fact]
//        public async Task GetByCategoryWithSuccess(){
//            produtoService.Setup(p => p.BuscarPorCategoria(It.IsAny<int>()))
//                        .ReturnsAsync(new Result<IEnumerable<RetornoProduto>>(new RetornoProduto[]{
//                            new() {},
//                            new() {},
//                            new() {},
//                            new() {}
//                        }));

//            var resultado = await produtosController.GetByCategory(1);

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task GetByCategoryWithSuccessButEmptyList(){
//            produtoService.Setup(p => p.BuscarPorCategoria(It.IsAny<int>()))
//                        .ReturnsAsync(new Result<IEnumerable<RetornoProduto>>(Array.Empty<RetornoProduto>()));

//            var resultado = await produtosController.GetByCategory(1);

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task GetByCategoryReturningBadRequest(){
//            produtoService.Setup(p => p.BuscarPorCategoria(It.IsAny<int>()))
//                        .ReturnsAsync(new Result<IEnumerable<RetornoProduto>>("Erro de lógica",true));
//            var resultado = await produtosController.GetByCategory(15);

//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Erro de lógica",payload.Message);
//        }

//        [Fact]
//        public async Task GetByCategoryWithThrowedException(){
//            produtoService.Setup(p => p.BuscarPorCategoria(It.IsAny<int>()))
//                        .ThrowsAsync(new Exception("Erro inesperado"));

//            var resultado = await produtosController.GetByCategory(2);

//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }

//        [Fact]
//        public async Task GetByCategoryWithReceivedException(){
//            produtoService.Setup(p => p.BuscarPorCategoria(It.IsAny<int>()))
//                        .ReturnsAsync(new Result<IEnumerable<RetornoProduto>>(new Exception("Erro inesperado")));

//            var resultado = await produtosController.GetByCategory(2);

//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }
//    }
//}