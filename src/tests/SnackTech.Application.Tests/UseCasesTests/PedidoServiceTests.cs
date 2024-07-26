using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Application.DTOs.Pedido;
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
        private readonly Mock<IClienteRepository> clienteRepository;
        private readonly Mock<IProdutoRepository> produtoRepository;
        private readonly PedidoService pedidoService;

        public PedidoServiceTests()
        {
            logger = new Mock<ILogger<PedidoService>>();
            pedidoRepository = new Mock<IPedidoRepository>();
            clienteRepository = new Mock<IClienteRepository>();
            produtoRepository = new Mock<IProdutoRepository>();
            pedidoService = new PedidoService(logger.Object, pedidoRepository.Object, clienteRepository.Object, produtoRepository.Object);
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

        [Fact]
        public async Task BuscarUltimoPedidoClienteWithSuccessAndObjects()
        {
            var cliente = new Cliente(Guid.NewGuid(), "Nome completo", "email@gmail.com", "582.202.320-72");
            var pedidoAtual = new Pedido(Guid.NewGuid(), DateTime.Now, StatusPedido.Iniciado, cliente, Array.Empty<PedidoItem>());
            pedidoRepository.Setup(p => p.PesquisarPorCliente(It.IsAny<String>()))
                                .ReturnsAsync(new List<Pedido>{
                                    pedidoAtual,
                                    new Pedido(Guid.NewGuid(), DateTime.Now.AddDays(-1), StatusPedido.AguardandoPagamento, cliente, Array.Empty<PedidoItem>()),
                                    new Pedido(Guid.NewGuid(), DateTime.Now.AddHours(-1), StatusPedido.Finalizado, cliente, Array.Empty<PedidoItem>()),
                                    new Pedido(Guid.NewGuid(), DateTime.Now.AddMinutes(-1), StatusPedido.EmPreparacao, cliente, Array.Empty<PedidoItem>())
                                });

            var resultado = await pedidoService.BuscarUltimoPedidoCliente(cliente.CPF);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.GetValue());
            Assert.Equal(pedidoAtual.Id.ToString(), resultado.GetValue().Identificacao);
        }

        [Fact]
        public async Task BuscarUltimoPedidoClienteWithSuccessButNoObjects()
        {
            var cpfCliente = "582.202.320-72";
            pedidoRepository.Setup(p => p.PesquisarPorCliente(It.IsAny<String>()))
                                .ReturnsAsync(Array.Empty<Pedido>());

            var resultado = await pedidoService.BuscarUltimoPedidoCliente(cpfCliente);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.Message);
            Assert.Contains($"Último Pedido do cliente com cpf {cpfCliente} não encontrado.", resultado.Message);
        }

        [Fact]
        public async Task BuscarUltimoPedidoClienteWithInvalidCpf()
        {
            var cpfCliente = "12312jjkj";
            var resultado = await pedidoService.BuscarUltimoPedidoCliente(cpfCliente);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.Contains($"cpfCliente com valor {cpfCliente} não é um CPF válido.", resultado.Message);
        }

        [Fact]
        public async Task BuscarUltimoPedidoClienteWithException()
        {
            pedidoRepository.Setup(p => p.PesquisarPorCliente(It.IsAny<String>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await pedidoService.BuscarUltimoPedidoCliente("582.202.320-72");

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains($"Erro inesperado", resultado.Message);
        }

        [Fact]
        public async Task ListarPedidosParaPagamentoWithSuccessAndObjects()
        {
            var cliente = new Cliente(Guid.NewGuid(), "Nome completo", "email@gmail.com", "582.202.320-72");
            pedidoRepository.Setup(p => p.PesquisarPedidosParaPagamento())
                                .ReturnsAsync(new List<Pedido>{
                                    new Pedido(Guid.NewGuid(), DateTime.Now.AddDays(-1), StatusPedido.AguardandoPagamento, cliente, Array.Empty<PedidoItem>()),
                                    new Pedido(Guid.NewGuid(), DateTime.Now.AddHours(-1), StatusPedido.AguardandoPagamento, cliente, Array.Empty<PedidoItem>()),
                                    new Pedido(Guid.NewGuid(), DateTime.Now.AddMinutes(-1), StatusPedido.AguardandoPagamento, cliente, Array.Empty<PedidoItem>())
                                });

            var resultado = await pedidoService.ListarPedidosParaPagamento();

            Assert.True(resultado.IsSuccess());
            Assert.True(resultado.Data.Any());
            Assert.Equal(3, resultado.Data.Count());
        }

        [Fact]
        public async Task ListarPedidosParaPagamentoWithSuccessButNoObjects()
        {
            pedidoRepository.Setup(p => p.PesquisarPedidosParaPagamento())
                                .ReturnsAsync(Array.Empty<Pedido>());

            var resultado = await pedidoService.ListarPedidosParaPagamento();

            Assert.True(resultado.IsSuccess());
            Assert.True(!resultado.Data.Any());
            Assert.Empty(resultado.Data);
        }

        [Fact]
        public async Task ListarPedidosParaPagamentoException()
        {
            var identificacao = Guid.NewGuid().ToString();
            pedidoRepository.Setup(p => p.PesquisarPedidosParaPagamento())
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await pedidoService.ListarPedidosParaPagamento();

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains($"Erro inesperado", resultado.Message);
        }

        [Fact]
        public async Task IniciarPedidoWithSuccess()
        {
            var cliente = new Cliente(Guid.NewGuid(), "Nome completo", "email@gmail.com", "582.202.320-72");
            clienteRepository.Setup(c => c.PesquisarPorCpf(cliente.CPF))
                            .ReturnsAsync(cliente);

            pedidoRepository.Setup(p => p.InserirPedido(It.IsAny<Pedido>()))
                                .Returns(Task.FromResult(0));

            var resultado = await pedidoService.IniciarPedido(cliente.CPF);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
        }

        [Fact]
        public async Task IniciarPedidoComClientePadraoWithSuccess()
        {
            var cliente = new Cliente(Guid.NewGuid(), "Padrao", "email@gmail.com", "123.456.789-09");
            clienteRepository.Setup(c => c.PesquisarClientePadrao())
                            .ReturnsAsync(cliente);

            pedidoRepository.Setup(p => p.InserirPedido(It.IsAny<Pedido>()))
                                .Returns(Task.FromResult(0));

            var resultado = await pedidoService.IniciarPedido(null);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
        }

        [Fact]
        public async Task IniciarPedidoWithArgumentException()
        {
            var cpfInvalido = "123451aaa";
            var resultado = await pedidoService.IniciarPedido(cpfInvalido);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.Contains($"cpfCliente com valor {cpfInvalido} não é um CPF válido.", resultado.Message);
        }

        [Fact]
        public async Task IniciarPedidoWithException()
        {
            var cliente = new Cliente(Guid.NewGuid(), "Nome completo", "email@gmail.com", "582.202.320-72");
            clienteRepository.Setup(c => c.PesquisarPorCpf(cliente.CPF))
                            .ReturnsAsync(cliente);

            pedidoRepository.Setup(p => p.InserirPedido(It.IsAny<Pedido>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await pedidoService.IniciarPedido(cliente.CPF);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains("Erro inesperado", resultado.Message);
        }

        [Fact]
        public async Task FinalizarPedidoParaPagamentoWithSuccess()
        {
            var identificacao = Guid.NewGuid();
            var identificacaoString = identificacao.ToString();
            var cliente = new Cliente(Guid.NewGuid(), "Nome completo", "email@gmail.com", "582.202.320-72");

            Produto produto = new Produto(CategoriaProduto.Lanche, "descricao", "Produto", 20);
            var pedidoItem = new PedidoItem(1, produto, 2, "");
            var pedido = new Pedido(identificacao, DateTime.Now, StatusPedido.Iniciado, cliente, new List<PedidoItem>() { pedidoItem });

            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);

            pedidoRepository.Setup(p => p.AtualizarPedido(pedido))
                            .Callback<Pedido>((obj) => Assert.Equal(StatusPedido.AguardandoPagamento, obj.Status))
                            .Returns(Task.CompletedTask);

            var resultado = await pedidoService.FinalizarPedidoParaPagamento(identificacao.ToString());

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);

            pedidoRepository.Verify(mock => mock.AtualizarPedido(pedido), Times.Once());
        }

        [Fact]
        public async Task FinalizarPedidoParaPagamentoSemItensReturnError()
        {
            var identificacao = Guid.NewGuid();
            var identificacaoString = identificacao.ToString();
            var cliente = new Cliente(Guid.NewGuid(), "Nome completo", "email@gmail.com", "582.202.320-72");
            var pedido = new Pedido(identificacao, DateTime.Now, StatusPedido.Iniciado, cliente, new List<PedidoItem>());

            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);

            var resultado = await pedidoService.FinalizarPedidoParaPagamento(identificacao.ToString());

            Assert.False(resultado.IsSuccess());
            Assert.Contains($"Pedido com identificação {identificacao.ToString()} não possui itens e não pode ser finalizado.", resultado.Message);
        }

        [Fact]
        public async Task FinalizarPedidoParaPagamentoSemItensNotFounded()
        {
            var identificacao = Guid.NewGuid();

            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(default(Pedido));

            var resultado = await pedidoService.FinalizarPedidoParaPagamento(identificacao.ToString());

            Assert.False(resultado.IsSuccess());
            Assert.Contains($"Pedido com identificação {identificacao.ToString()} não encontrado.", resultado.Message);
        }

        [Fact]
        public async Task FinalizarPedidoParaPagamentoInvalidGuid()
        {
            var identificacao = "";

            var resultado = await pedidoService.FinalizarPedidoParaPagamento(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.Message);
            Assert.Contains("não é um Guid válido.", resultado.Message);
        }

        [Fact]
        public async Task FinalizarPedidoParaPagamentoException()
        {
            var identificacao = Guid.NewGuid().ToString();
            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await pedidoService.FinalizarPedidoParaPagamento(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains($"Erro inesperado", resultado.Message);
        }

        [Fact]
        public async Task AtualizarPedidoAdicionandoItensWithSuccess()
        {
            var identificacao = Guid.NewGuid();
            var identificacaoString = identificacao.ToString();
            var cliente = new Cliente(Guid.NewGuid(), "Nome completo", "email@gmail.com", "582.202.320-72");
            var produto = new Produto(CategoriaProduto.Lanche, "descricao", "Produto", 20);
            var produto2 = new Produto(CategoriaProduto.Bebida, "descricao", "Produto2", 10);
            produtoRepository.Setup(p => p.PesquisarPorId(produto.Id))
                            .ReturnsAsync(produto);
            produtoRepository.Setup(p => p.PesquisarPorId(produto2.Id))
                            .ReturnsAsync(produto2);

            var pedido = new Pedido(identificacao, DateTime.Now, StatusPedido.AguardandoPagamento, cliente, new List<PedidoItem>());
            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);
            pedidoRepository.Setup(p => p.AtualizarPedido(It.IsAny<Pedido>()))
                            .Callback<Pedido>((pedidoAtualizado) => Assert.Equal(2, pedidoAtualizado.Itens.Count))
                            .Returns(Task.CompletedTask);

            var pedidoItemAtualizado = new AtualizacaoPedidoItem
            {
                Sequencial = 1,
                IdentificacaoProduto = produto.Id.ToString(),
                Quantidade = 2,
                Observacao = "aaa"
            };
            var pedidoItemAtualizado2 = new AtualizacaoPedidoItem
            {
                Sequencial = 2,
                IdentificacaoProduto = produto.Id.ToString(),
                Quantidade = 1,
                Observacao = ""
            };

            var AtualizacaoPedido = new AtualizacaoPedido
            {
                Identificacao = identificacaoString,
                PedidoItens = new List<AtualizacaoPedidoItem> { pedidoItemAtualizado, pedidoItemAtualizado2 }
            };

            var resultado = await pedidoService.AtualizarPedido(AtualizacaoPedido);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
        }
        [Fact]
        public async Task AtualizarPedidoAtualizandoItensExistentesWithSuccess()
        {
            var identificacao = Guid.NewGuid();
            var identificacaoString = identificacao.ToString();
            var cliente = new Cliente(Guid.NewGuid(), "Nome completo", "email@gmail.com", "582.202.320-72");
            var produto = new Produto(CategoriaProduto.Lanche, "descricao", "Produto", 20);
            var produto2 = new Produto(CategoriaProduto.Bebida, "descricao", "Produto2", 10);
            produtoRepository.Setup(p => p.PesquisarPorId(produto.Id))
                            .ReturnsAsync(produto);
            produtoRepository.Setup(p => p.PesquisarPorId(produto2.Id))
                            .ReturnsAsync(produto2);

            var pedidoItem = new PedidoItem(1, produto, 1, "");
            var pedido = new Pedido(identificacao, DateTime.Now, StatusPedido.AguardandoPagamento, cliente, new List<PedidoItem>() { pedidoItem });
            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);
            pedidoRepository.Setup(p => p.AtualizarPedido(It.IsAny<Pedido>()))
                            .Callback<Pedido>((pedidoAtualizado) =>
                                {
                                    Assert.Equal(1, pedidoAtualizado.Itens.Count);
                                    Assert.Equal(2, pedidoAtualizado.Itens.First().Quantidade);
                                    Assert.Equal("observacao", pedidoAtualizado.Itens.First().Observacao);
                                })
                            .Returns(Task.CompletedTask);

            var pedidoItemAtualizado = new AtualizacaoPedidoItem
            {
                Sequencial = 1,
                IdentificacaoProduto = produto.Id.ToString(),
                Quantidade = 2,
                Observacao = "observacao"
            };

            var AtualizacaoPedido = new AtualizacaoPedido
            {
                Identificacao = identificacaoString,
                PedidoItens = new List<AtualizacaoPedidoItem> { pedidoItemAtualizado }
            };

            var resultado = await pedidoService.AtualizarPedido(AtualizacaoPedido);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
        }
        [Fact]
        public async Task AtualizarPedidoRemovendoItensExistentesWithSuccess()
        {
            var identificacao = Guid.NewGuid();
            var identificacaoString = identificacao.ToString();
            var cliente = new Cliente(Guid.NewGuid(), "Nome completo", "email@gmail.com", "582.202.320-72");
            var produto = new Produto(CategoriaProduto.Lanche, "descricao", "Produto", 20);
            var produto2 = new Produto(CategoriaProduto.Bebida, "descricao", "Produto2", 10);
            produtoRepository.Setup(p => p.PesquisarPorId(produto.Id))
                            .ReturnsAsync(produto);
            produtoRepository.Setup(p => p.PesquisarPorId(produto2.Id))
                            .ReturnsAsync(produto2);

            var pedidoItem1 = new PedidoItem(1, produto, 2, "remover");
            var pedidoItem2 = new PedidoItem(2, produto2, 1, "manter");
            var pedido = new Pedido(identificacao, DateTime.Now, StatusPedido.AguardandoPagamento, cliente, new List<PedidoItem>() { pedidoItem1, pedidoItem2 });
            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);
            pedidoRepository.Setup(p => p.AtualizarPedido(It.IsAny<Pedido>()))
                            .Callback<Pedido>((pedidoAtualizado) =>
                            {
                                Assert.Equal(1, pedidoAtualizado.Itens.Count);
                                Assert.Equal(pedidoItem2.Sequencial, pedidoAtualizado.Itens.First().Sequencial);
                                Assert.Equal(pedidoItem2.Observacao, pedidoAtualizado.Itens.First().Observacao);
                            })
                            .Returns(Task.CompletedTask);

            var pedidoItemAtualizado = new AtualizacaoPedidoItem
            {
                Sequencial = pedidoItem2.Sequencial,
                IdentificacaoProduto = pedidoItem2.Produto.Id.ToString(),
                Quantidade = pedidoItem2.Quantidade,
                Observacao = pedidoItem2.Observacao,
            };

            var AtualizacaoPedido = new AtualizacaoPedido
            {
                Identificacao = identificacaoString,
                PedidoItens = new List<AtualizacaoPedidoItem> { pedidoItemAtualizado }
            };

            var resultado = await pedidoService.AtualizarPedido(AtualizacaoPedido);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
        }

        [Fact]
        public async Task AtualizarPedidoWithInvalidGuid()
        {
            var AtualizacaoPedido = new AtualizacaoPedido
            {
                Identificacao = "aaaaaaaaaaaaaaa",
                PedidoItens = new List<AtualizacaoPedidoItem>()
            };

            var resultado = await pedidoService.AtualizarPedido(AtualizacaoPedido);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.Contains($"Identificacao não é um Guid válido", resultado.Message);
        }

        [Fact]
        public async Task AtualizarPedidoException()
        {
            pedidoRepository.Setup(p => p.PesquisarPorId(It.IsAny<Guid>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));
            var identificacao = Guid.NewGuid().ToString();

            var resultado = await pedidoService.FinalizarPedidoParaPagamento(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains($"Erro inesperado", resultado.Message);
        }
    }
}
