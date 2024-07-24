using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Application.UseCases;
using SnackTech.Domain.Contracts;
using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;

namespace SnackTech.Application.Tests.UseCasesTests
{
    public class PedidoServiceTests
    {
        private readonly Mock<ILogger<PedidoService>> logger;
        private readonly Mock<IPedidoRepository> pedidoRepository;
        private readonly PedidoService pedidoService;

        public PedidoServiceTests()
        {
            logger = new Mock<ILogger<PedidoService>>();
            pedidoRepository = new Mock<IPedidoRepository>();
            pedidoService = new PedidoService(logger.Object, pedidoRepository.Object);
        }

        [Fact]
        public async Task BuscarPedidoPorIdentificacaoWithSuccess()
        {
            var identificacao = Guid.NewGuid();
            var identificacaoString = identificacao.ToString();
            var cliente = new Cliente(Guid.NewGuid(), "Nome completo", "email@gmail.com", "582.202.320-72");
            var pedido = new Pedido(identificacao, DateTime.Now, StatusPedido.AguardandoPagamento, cliente, Array.Empty<PedidoItem>());
            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);

            var resultado = await pedidoService.BuscarPorIdenticacao(identificacaoString);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.GetValue());
            Assert.Equal(identificacaoString, resultado.GetValue().Identificacao);
        }

        [Fact]
        public async Task BuscarPedidoPorIdentificacaoInvalidGuid()
        {
            var identificacao = "";

            var resultado = await pedidoService.BuscarPorIdenticacao(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.Message);
            Assert.Contains("não é um Guid válido.", resultado.Message);
        }

        [Fact]
        public async Task BuscarPedidoPorIdentificacaoNotFounded()
        {
            var identificacao = Guid.NewGuid().ToString();
            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(default(Pedido));

            var resultado = await pedidoService.BuscarPorIdenticacao(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.Message);
            Assert.Contains($"Pedido com identificação {identificacao} não encontrado.", resultado.Message);
        }

        [Fact]
        public async Task BuscarPedidoPorIdentificacaoException()
        {
            var identificacao = Guid.NewGuid().ToString();
            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await pedidoService.BuscarPorIdenticacao(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains($"Erro inesperado", resultado.Message);
        }
    }
}
