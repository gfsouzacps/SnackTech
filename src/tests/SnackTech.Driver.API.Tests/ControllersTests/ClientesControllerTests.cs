//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using SnackTech.Driver.API.Controllers;
//using SnackTech.Driver.API.CustomResponses;
//using SnackTech.Domain.Common;
//using SnackTech.Domain.DTOs.Driving.Cliente;
//using SnackTech.Domain.Ports.Driving;

//namespace SnackTech.Driver.API.Tests.ControllersTests
//{
//    public class ClientesControllerTests
//    {
//        private readonly Mock<ILogger<ClientesController>> logger;
//        private readonly Mock<IClienteService> clienteService;
//        private readonly ClientesController clientesController;
//        public ClientesControllerTests(){
//            logger = new Mock<ILogger<ClientesController>>();
//            clienteService = new Mock<IClienteService>();
//            clientesController = new ClientesController(logger.Object, clienteService.Object);
//        }

//        [Fact]
//        public async Task PostWithSuccess(){
//            var retornoCliente = new RetornoCliente{};
//            var cadastroCliente = new CadastroCliente();
//            clienteService.Setup(c => c.Cadastrar(It.IsAny<CadastroCliente>()))
//                            .ReturnsAsync(new Result<RetornoCliente>(retornoCliente));

//            var resultado = await clientesController.Post(cadastroCliente);

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task PostWithBadRequest(){
//            var cadastroCliente = new CadastroCliente();
//            clienteService.Setup(c => c.Cadastrar(It.IsAny<CadastroCliente>()))
//                            .ReturnsAsync(new Result<RetornoCliente>("Erro de lógica",true));

//            var resultado = await clientesController.Post(cadastroCliente);

//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Erro de lógica",payload.Message);
//        }

//        [Fact]
//        public async Task PostWithInternalServerError(){
//            var cadastroCliente = new CadastroCliente();
//            clienteService.Setup(c => c.Cadastrar(It.IsAny<CadastroCliente>()))
//                            .ThrowsAsync(new Exception("Erro inesperado"));
            
//            var resultado = await clientesController.Post(cadastroCliente);
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }

//        [Fact]
//        public async Task GetByCpfWithSuccess(){
//            var cpf = "42572271095";
//            var retornoCliente = new RetornoCliente{};
//            clienteService.Setup(c => c.IdentificarPorCpf(It.IsAny<string>()))
//                            .ReturnsAsync(new Result<RetornoCliente>(retornoCliente));

//            var resultado = await clientesController.GetByCpf(cpf);

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task GetByCpfWithBadRequest(){
//             var cpf = "42572271095";
//            clienteService.Setup(c => c.IdentificarPorCpf(It.IsAny<string>()))
//                            .ReturnsAsync(new Result<RetornoCliente>("Erro de lógica",true));

//            var resultado = await clientesController.GetByCpf(cpf);

//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Erro de lógica",payload.Message);
//        }

//        [Fact]
//        public async Task GetByCpfWithInternalServerError(){
//            var cpf = "42572271095";
//            clienteService.Setup(c => c.IdentificarPorCpf(It.IsAny<string>()))
//                            .ThrowsAsync(new Exception("Erro inesperado"));
            
//            var resultado = await clientesController.GetByCpf(cpf);
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }

//        [Fact]
//        public async Task GetDefaultClientWithSuccess(){
//            var retornoCliente = new RetornoCliente{};
//            clienteService.Setup(c => c.SelecionarClientePadrao())
//                            .ReturnsAsync(new Result<RetornoCliente>(retornoCliente));

//            var resultado = await clientesController.GetDefaultClient();

//            Assert.IsType<OkObjectResult>(resultado);
//        }

//        [Fact]
//        public async Task GetDefaultClientWithBadRequest(){

//            clienteService.Setup(c => c.SelecionarClientePadrao())
//                            .ReturnsAsync(new Result<RetornoCliente>("Erro de lógica",true));

//            var resultado = await clientesController.GetDefaultClient();

//            var objectResult = Assert.IsType<BadRequestObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);
//            Assert.Null(payload.Exception);
//            Assert.Equal("Erro de lógica",payload.Message);
//        }

//        [Fact]
//        public async Task GetDefaultClientWithInternalServerError(){
//            clienteService.Setup(c => c.SelecionarClientePadrao())
//                            .ThrowsAsync(new Exception("Erro inesperado"));
            
//            var resultado = await clientesController.GetDefaultClient();
//            var objectResult = Assert.IsType<ObjectResult>(resultado);
//            var payload = Assert.IsType<ErrorResponse>(objectResult.Value);

//            Assert.NotNull(payload.Exception);
//            Assert.Equal("Erro inesperado",payload.Message);
//        }
//    }
//}