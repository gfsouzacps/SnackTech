using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Application.UseCases;
using SnackTech.Domain.DTOs.Cliente;
using SnackTech.Domain.Models;
using SnackTech.Domain.Ports.Driven;

namespace SnackTech.Application.Tests.UseCasesTests
{
    public class ClienteServiceTests
    {
        private readonly Mock<ILogger<ClienteService>> logger;
        private readonly Mock<IClienteRepository> clienteRepository;
        private readonly ClienteService clienteService;

        public ClienteServiceTests(){
            logger = new Mock<ILogger<ClienteService>>();
            clienteRepository = new Mock<IClienteRepository>();
            clienteService = new ClienteService(logger.Object,clienteRepository.Object);
        }

        [Fact]
        public async Task CadastrarWithSuccess(){
            var cadastroCliente = new CadastroCliente{
                Nome = "Nome completo",
                Email = "email@gmail.com",
                CPF = "582.202.320-72"
            };

            clienteRepository
                .Setup(s => s.InserirClienteAsync(It.IsAny<Cliente>()))
                .Returns(Task.FromResult(0));

            var resultado = await clienteService.Cadastrar(cadastroCliente);

            Assert.True(resultado.IsSuccess());
            Assert.NotNull(resultado.Data);
            Assert.Equal("Nome completo",resultado.Data.Nome);
            Assert.NotNull(resultado.Data.Id.ToString());

        }

        [Fact]
        public async Task CadastrarWithArgumentException(){
            var cadastroCliente = new CadastroCliente{
                Nome = "Nome completo",
                Email = "email@gmail.com",
                CPF = "cpfinvalido"
            };

            var resultado = await clienteService.Cadastrar(cadastroCliente);

            Assert.False(resultado.IsSuccess());
            Assert.False(resultado.HasException());
            Assert.Equal("cpf gerou erro ao ser validado como CPF. (Parameter 'cpf')",resultado.Message);
            
            logger.Verify(l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state,type) => (state.ToString() ?? "").Contains("ClienteService.Cadastrar - ArgumentException")),
                It.IsAny<Exception>(),
                (Func<object, Exception?, string>)It.IsAny<object>()
            ), Times.Once);
        }

        [Fact]
        public async Task CadastrarWithException(){
            var cadastroCliente = new CadastroCliente{
                Nome = "Nome completo",
                Email = "email@gmail.com",
                CPF = "582.202.320-72"
            };

            clienteRepository
                .Setup(c => c.InserirClienteAsync(It.IsAny<Cliente>()))
                .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await clienteService.Cadastrar(cadastroCliente);

            Assert.False(resultado.IsSuccess());
            Assert.True(resultado.HasException());
            Assert.NotNull(resultado.Exception);
            Assert.Equal("Erro inesperado",resultado.Exception.Message);

            logger.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state,type) => (state.ToString() ?? "").Contains("ClienteService.Cadastrar - Exception")),
                It.IsAny<Exception>(),
                (Func<object, Exception?, string>)It.IsAny<object>()
            ), Times.Once);
        }

        [Fact]
        public async Task IdentificarPorCpfWithSuccess(){
            var cpf = "582.202.320-72";
            var retorno = new Cliente("Nome","email@gmail.com",cpf);

            clienteRepository.Setup(c => c.PesquisarPorCpfAsync(It.IsAny<string>()))
                            .ReturnsAsync(retorno);

            var resultado = await clienteService.IdentificarPorCpf(cpf);

            Assert.True(resultado.IsSuccess());
            Assert.NotNull(resultado.Data);
            Assert.Equal("Nome",resultado.Data.Nome);
        }

        [Fact]
        public async Task IdentificarPorCpfNotFoundingClient(){
            var cpf = "582.202.320-72";

            clienteRepository.Setup(c => c.PesquisarPorCpfAsync(It.IsAny<string>()))
                            .ReturnsAsync(default(Cliente));

            var resultado = await clienteService.IdentificarPorCpf(cpf);

            Assert.False(resultado.IsSuccess());
            Assert.False(resultado.HasException());
            Assert.Null(resultado.Data);
            Assert.Null(resultado.Exception);
            Assert.Equal($"{cpf} não encontrado.",resultado.Message);
        }

        [Fact]
        public async Task IdentificarPorCpfWithException(){
            var cpf = "582.202.320-72";

            clienteRepository.Setup(c =>( c.PesquisarPorCpfAsync(It.IsAny<string>())))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await clienteService.IdentificarPorCpf(cpf);

            Assert.False(resultado.IsSuccess());
            Assert.True(resultado.HasException());
            Assert.NotNull(resultado.Exception);
            Assert.Equal("Erro inesperado",resultado.Exception.Message);

            logger.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state,type) => (state.ToString() ?? "").Contains("ClienteService.IdentificarPorCpf")),
                It.IsAny<Exception>(),
                (Func<object, Exception?, string>)It.IsAny<object>()
            ), Times.Once);
        }

        [Fact]
        public async Task SelecionarClientePadraoWithSuccess(){
            var cpf = "582.202.320-72";
            var retorno = new Cliente("Usuario padrao","email@gmail.com",cpf);

            clienteRepository.Setup(c => c.PesquisarClientePadraoAsync())
                            .ReturnsAsync(retorno);

            var resultado = await clienteService.SelecionarClientePadrao();

            Assert.True(resultado.IsSuccess());
        }

        [Fact]
        public async Task SelecionarClientePadraoWithException(){
            clienteRepository.Setup(c => c.PesquisarClientePadraoAsync())
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await clienteService.SelecionarClientePadrao();

            Assert.False(resultado.IsSuccess());
            Assert.True(resultado.HasException());
            Assert.NotNull(resultado.Exception);
            Assert.Equal("Erro inesperado",resultado.Exception.Message);

            logger.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state,type) => (state.ToString() ?? "").Contains("ClienteService.SelecionarClientePadrao - Exception")),
                It.IsAny<Exception>(),
                (Func<object, Exception?, string>)It.IsAny<object>()
            ), Times.Once);
        }
    }
}