//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using SnackTech.Driver.API.Controllers;
//using SnackTech.Driver.API.CustomResponses;
//using SnackTech.Domain.Common;
//using SnackTech.Domain.DTOs.Driving.Cliente;
//using SnackTech.Domain.DTOs.Driving.Pedido;
//using SnackTech.Domain.Ports.Driving;

//namespace SnackTech.Driver.API.Tests.ControllersTests
//{
//    public class PedidosControllerTests
//    {
//        private readonly Mock<ILogger<PedidosController>> logger;
//        private readonly Mock<IPedidoService> pedidoService;
//        private readonly PedidosController pedidosController;
//        public PedidosControllerTests(){
//            logger = new Mock<ILogger<PedidosController>>();
//            pedidoService = new Mock<IPedidoService>();
//            pedidosController = new PedidosController(logger.Object, pedidoService.Object);
//        }

//        [Fact]
//        public async Task IniciarPedidoWithSuccess(){
//            pedidoService.Setup(c => c.IniciarPedido(It.IsAny<string>()))
//                            .ReturnsAsync(new Result<Guid>(Guid.NewGuid()));

//            var iniciarPedido = new IniciarPedido{
//                Cpf = "1"
//            };
//            var resultado = await pedidosController.IniciarPedido(iniciarPedido);

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task IniciarPedidoWithBadRequest()
//        {
//            pedidoService.Setup(c => c.IniciarPedido(It.IsAny<string>()))
//                            .ReturnsAsync(new Result<Guid>("Erro de lógica", true));

//            var iniciarPedido = new IniciarPedido{
//                Cpf = "1"
//            };
//            var resultado = await pedidosController.IniciarPedido(iniciarPedido);

//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Erro de lógica", payload.Message);
//        }

//        [Fact]
//        public async Task IniciarPedidoWithInternalServerError()
//        {
//            var cadastroCliente = new CadastroCliente();
//            pedidoService.Setup(c => c.IniciarPedido(It.IsAny<string>()))
//                            .ThrowsAsync(new Exception("Erro inesperado"));
            
//            var iniciarPedido = new IniciarPedido{
//                Cpf = "1"
//            };
//            var resultado = await pedidosController.IniciarPedido(iniciarPedido);
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado", payload.Message);
//        }

//        [Fact]
//        public async Task AtualizarPedidoWithSuccess()
//        {
//            pedidoService.Setup(c => c.AtualizarPedido(It.IsAny<AtualizacaoPedido>()))
//                            .ReturnsAsync(new Result());

//            var atualizacaoPedido = new AtualizacaoPedido() { PedidoItens = new List<AtualizacaoPedidoItem>() };

//            var resultado = await pedidosController.AtualizarPedido(atualizacaoPedido);

//            Assert.IsType<OkResult>(resultado);
//        }

//        [Fact]
//        public async Task AtualizarPedidoWithBadRequest()
//        {
//            pedidoService.Setup(c => c.AtualizarPedido(It.IsAny<AtualizacaoPedido>()))
//                            .ReturnsAsync(new Result("Erro de lógica"));

//            var atualizacaoPedido = new AtualizacaoPedido() { PedidoItens = new List<AtualizacaoPedidoItem>() };

//            var resultado = await pedidosController.AtualizarPedido(atualizacaoPedido);

//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Erro de lógica", payload.Message);
//        }

//        [Fact]
//        public async Task AtualizarPedidoWithInternalServerError()
//        {
//            var cadastroCliente = new CadastroCliente();
//            pedidoService.Setup(c => c.AtualizarPedido(It.IsAny<AtualizacaoPedido>()))
//                            .ThrowsAsync(new Exception("Erro inesperado"));

//            var atualizacaoPedido = new AtualizacaoPedido() { PedidoItens = new List<AtualizacaoPedidoItem>() };

//            var resultado = await pedidosController.AtualizarPedido(atualizacaoPedido);
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado", payload.Message);
//        }

//        [Fact]
//        public async Task FinalizarPedidoParaPagamentoWithSuccess()
//        {
//            pedidoService.Setup(c => c.FinalizarPedidoParaPagamento(It.IsAny<string>()))
//                            .ReturnsAsync(new Result());

//            var resultado = await pedidosController.FinalizarPedidoParaPagamento("1");

//            Assert.IsType<OkResult>(resultado);
//        }

//        [Fact]
//        public async Task FinalizarPedidoParaPagamentoWithBadRequest()
//        {
//            pedidoService.Setup(c => c.FinalizarPedidoParaPagamento(It.IsAny<string>()))
//                            .ReturnsAsync(new Result("Erro de lógica"));

//            var resultado = await pedidosController.FinalizarPedidoParaPagamento("1");

//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Erro de lógica", payload.Message);
//        }

//        [Fact]
//        public async Task FinalizarPedidoParaPagamentoWithInternalServerError()
//        {
//            var cadastroCliente = new CadastroCliente();
//            pedidoService.Setup(c => c.FinalizarPedidoParaPagamento(It.IsAny<string>()))
//                            .ThrowsAsync(new Exception("Erro inesperado"));

//            var resultado = await pedidosController.FinalizarPedidoParaPagamento("1");
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado", payload.Message);
//        }

//        [Fact]
//        public async Task BuscarPorIdenticacaoWithSuccess()
//        {
//            var retornoPedido = new RetornoPedido { Itens = new List<RetornoPedidoItem>() };
//            pedidoService.Setup(c => c.BuscarPorIdenticacao(It.IsAny<string>()))
//                            .ReturnsAsync(new Result<RetornoPedido>(retornoPedido));

//            var resultado = await pedidosController.BuscarPorIdenticacao("11");

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task BuscarPorIdenticacaoWithBadRequest()
//        {
//            var cpf = "42572271095";
//            pedidoService.Setup(c => c.BuscarPorIdenticacao(It.IsAny<string>()))
//                            .ReturnsAsync(new Result<RetornoPedido>("Erro de lógica", true));

//            var resultado = await pedidosController.BuscarPorIdenticacao(cpf);

//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Erro de lógica", payload.Message);
//        }

//        [Fact]
//        public async Task BuscarPorIdenticacaoWithInternalServerError()
//        {
//            var cpf = "42572271095";
//            pedidoService.Setup(c => c.BuscarPorIdenticacao(It.IsAny<string>()))
//                            .ThrowsAsync(new Exception("Erro inesperado"));

//            var resultado = await pedidosController.BuscarPorIdenticacao(cpf);
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado", payload.Message);
//        }

//        [Fact]
//        public async Task BuscarUltimoPedidoClienteWithSuccess()
//        {
//            var retornoPedido = new RetornoPedido { Itens = new List<RetornoPedidoItem>() };
//            pedidoService.Setup(c => c.BuscarUltimoPedidoCliente(It.IsAny<string>()))
//                            .ReturnsAsync(new Result<RetornoPedido>(retornoPedido));

//            var resultado = await pedidosController.BuscarUltimoPedidoCliente("11");

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task BuscarUltimoPedidoClienteWithBadRequest()
//        {
//            var cpf = "42572271095";
//            pedidoService.Setup(c => c.BuscarUltimoPedidoCliente(It.IsAny<string>()))
//                            .ReturnsAsync(new Result<RetornoPedido>("Erro de lógica", true));

//            var resultado = await pedidosController.BuscarUltimoPedidoCliente(cpf);

//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Erro de lógica", payload.Message);
//        }

//        [Fact]
//        public async Task BuscarUltimoPedidoClienteWithInternalServerError()
//        {
//            var cpf = "42572271095";
//            pedidoService.Setup(c => c.BuscarUltimoPedidoCliente(It.IsAny<string>()))
//                            .ThrowsAsync(new Exception("Erro inesperado"));

//            var resultado = await pedidosController.BuscarUltimoPedidoCliente(cpf);
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado", payload.Message);
//        }

//        [Fact]
//        public async Task ListarPedidosParaPagamentoWithSuccess()
//        {
//            pedidoService.Setup(p => p.ListarPedidosParaPagamento())
//                        .ReturnsAsync(new Result<IEnumerable<RetornoPedido>>(new RetornoPedido[]{
//                            new RetornoPedido { Itens = new List<RetornoPedidoItem>() },
//                            new RetornoPedido { Itens = new List<RetornoPedidoItem>() },
//                            new RetornoPedido { Itens = new List<RetornoPedidoItem>() },
//                            new RetornoPedido { Itens = new List<RetornoPedidoItem>() }
//                        }));

//            var resultado = await pedidosController.ListarPedidosParaPagamento();

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task ListarPedidosParaPagamentoWithSuccessButEmptyList()
//        {
//            pedidoService.Setup(p => p.ListarPedidosParaPagamento())
//                        .ReturnsAsync(new Result<IEnumerable<RetornoPedido>>(Array.Empty<RetornoPedido>()));

//            var resultado = await pedidosController.ListarPedidosParaPagamento();

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task GetByCategoryReturningBadRequest()
//        {
//            pedidoService.Setup(p => p.ListarPedidosParaPagamento())
//                        .ReturnsAsync(new Result<IEnumerable<RetornoPedido>>("Erro de lógica", true));
//            var resultado = await pedidosController.ListarPedidosParaPagamento();

//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Erro de lógica", payload.Message);
//        }

//        [Fact]
//        public async Task GetByCategoryWithThrowedException()
//        {
//            pedidoService.Setup(p => p.ListarPedidosParaPagamento())
//                        .ThrowsAsync(new Exception("Erro inesperado"));

//            var resultado = await pedidosController.ListarPedidosParaPagamento();

//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado", payload.Message);
//        }

//        [Fact]
//        public async Task GetByCategoryWithReceivedException()
//        {
//            pedidoService.Setup(p => p.ListarPedidosParaPagamento())
//                        .ReturnsAsync(new Result<IEnumerable<RetornoPedido>>(new Exception("Erro inesperado")));

//            var resultado = await pedidosController.ListarPedidosParaPagamento();

//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado", payload.Message);
//        }
//    }
//}