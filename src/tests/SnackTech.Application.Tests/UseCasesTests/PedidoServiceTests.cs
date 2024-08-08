using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Domain.DTOs.Pedido;
using SnackTech.Application.UseCases;
using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;
using SnackTech.Domain.Ports.Driven;

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
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");
            var pedido = new Pedido(cliente);
            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);

            var resultado = await pedidoService.BuscarPorIdenticacao(cliente.Id.ToString());

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.GetValue());
            Assert.Equal(pedido.Id.ToString(), resultado.GetValue().Identificacao);
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
            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
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
            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await pedidoService.BuscarPorIdenticacao(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains($"Erro inesperado", resultado.Message);
        }

        [Fact]
        public async Task BuscarUltimoPedidoClienteWithSuccessAndObjects()
        {
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");
            var pedidoAnterior = new Pedido(cliente);
            await Task.Delay(500); //esperar meio segundo antes do proximo pedido para ter uma diferença entre o horario de criação de cada um
            var pedidoAtual = new Pedido(cliente);
            
            clienteRepository.Setup(c => c.PesquisarPorCpfAsync(cliente.Cpf))
                            .ReturnsAsync(cliente);

            pedidoRepository.Setup(p => p.PesquisarPorClienteAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(new List<Pedido>{
                                    pedidoAtual,
                                    pedidoAnterior
                                });

            var resultado = await pedidoService.BuscarUltimoPedidoCliente(cliente.Cpf);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.GetValue());
            Assert.Equal(pedidoAtual.Id.ToString(), resultado.GetValue().Identificacao);
        }

        [Fact]
        public async Task BuscarUltimoPedidoClienteWithSuccessButNoObjects()
        {
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");

            clienteRepository.Setup(c => c.PesquisarPorCpfAsync(cliente.Cpf))
                            .ReturnsAsync(cliente);

            pedidoRepository.Setup(p => p.PesquisarPorClienteAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(Array.Empty<Pedido>());

            var resultado = await pedidoService.BuscarUltimoPedidoCliente(cliente.Cpf);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.NotNull(resultado.Message);
            Assert.Contains($"Último Pedido do cliente com cpf {cliente.Cpf} não encontrado.", resultado.Message);
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
        public async Task BuscarUltimoPedidoClienteWithClientePadraoThrowsException()
        {
            var cliente = new Cliente("Cliente padrao", "email@gmail.com", Cliente.CPF_CLIENTE_PADRAO);

            clienteRepository.Setup(c => c.PesquisarPorCpfAsync(cliente.Cpf))
                            .ReturnsAsync(cliente);

            var resultado = await pedidoService.BuscarUltimoPedidoCliente(Cliente.CPF_CLIENTE_PADRAO);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.Contains($"Não é permitido consultar o último pedido do cliente padrão.", resultado.Message);
        }

        [Fact]
        public async Task BuscarUltimoPedidoClienteWithException()
        {
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");

            clienteRepository.Setup(c => c.PesquisarPorCpfAsync(cliente.Cpf))
                            .ReturnsAsync(cliente);

            pedidoRepository.Setup(p => p.PesquisarPorClienteAsync(It.IsAny<Guid>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await pedidoService.BuscarUltimoPedidoCliente(cliente.Cpf);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains($"Erro inesperado", resultado.Message);
        }

        [Fact]
        public async Task ListarPedidosParaPagamentoWithSuccessAndObjects()
        {
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");
            var pedido1 = new Pedido(cliente);
            pedido1.FecharPedidoParaPagamento();
            var pedido2 = new Pedido(cliente);
            pedido2.FecharPedidoParaPagamento();
            pedidoRepository.Setup(p => p.PesquisarPedidosParaPagamentoAsync())
                                .ReturnsAsync(new List<Pedido>{
                                    pedido1,
                                    pedido2
                                });

            var resultado = await pedidoService.ListarPedidosParaPagamento();

            Assert.True(resultado.IsSuccess());
            Assert.True(resultado.Data.Any());
            Assert.Equal(2, resultado.Data.Count());
        }

        [Fact]
        public async Task ListarPedidosParaPagamentoWithSuccessButNoObjects()
        {
            pedidoRepository.Setup(p => p.PesquisarPedidosParaPagamentoAsync())
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
            pedidoRepository.Setup(p => p.PesquisarPedidosParaPagamentoAsync())
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await pedidoService.ListarPedidosParaPagamento();

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains($"Erro inesperado", resultado.Message);
        }

        [Fact]
        public async Task IniciarPedidoWithSuccess()
        {
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");
            clienteRepository.Setup(c => c.PesquisarPorCpfAsync(cliente.Cpf))
                            .ReturnsAsync(cliente);

            pedidoRepository.Setup(p => p.InserirPedidoAsync(It.IsAny<Pedido>()))
                                .Returns(Task.FromResult(0));

            var resultado = await pedidoService.IniciarPedido(cliente.Cpf);

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
        }

        [Fact]
        public async Task IniciarPedidoComClientePadraoWithSuccess()
        {
            var cliente = new Cliente("Padrao", "email@gmail.com", "123.456.789-09");
            clienteRepository.Setup(c => c.PesquisarClientePadraoAsync())
                            .ReturnsAsync(cliente);

            pedidoRepository.Setup(p => p.InserirPedidoAsync(It.IsAny<Pedido>()))
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
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");
            clienteRepository.Setup(c => c.PesquisarPorCpfAsync(cliente.Cpf))
                            .ReturnsAsync(cliente);

            pedidoRepository.Setup(p => p.InserirPedidoAsync(It.IsAny<Pedido>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));

            var resultado = await pedidoService.IniciarPedido(cliente.Cpf);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains("Erro inesperado", resultado.Message);
        }

        [Fact]
        public async Task FinalizarPedidoParaPagamentoWithSuccess()
        {
            var identificacao = Guid.NewGuid();
            var identificacaoString = identificacao.ToString();
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");

            Produto produto = new Produto(CategoriaProduto.Lanche, "descricao", "Produto", 20);
            var pedido = new Pedido(cliente);
            pedido.AdicionarItem(produto, 2, "");

            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);

            pedidoRepository.Setup(p => p.AtualizarPedidoAsync(pedido))
                            .Callback<Pedido>((obj) => Assert.Equal(StatusPedido.AguardandoPagamento, obj.Status))
                            .Returns(Task.CompletedTask);

            var resultado = await pedidoService.FinalizarPedidoParaPagamento(identificacao.ToString());

            Assert.True(resultado.IsSuccess());
            Assert.Null(resultado.Exception);

            pedidoRepository.Verify(mock => mock.AtualizarPedidoAsync(pedido), Times.Once());
        }

        [Fact]
        public async Task FinalizarPedidoParaPagamentoSemItensReturnError()
        {
            var identificacao = Guid.NewGuid();
            var identificacaoString = identificacao.ToString();
            var cliente = new Cliente( "Nome completo", "email@gmail.com", "582.202.320-72");
            var pedido = new Pedido(cliente);

            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);

            var resultado = await pedidoService.FinalizarPedidoParaPagamento(identificacao.ToString());

            Assert.False(resultado.IsSuccess());
            Assert.Contains($"Pedido com identificação {identificacao.ToString()} não possui itens e não pode ser finalizado.", resultado.Message);
        }

        [Fact]
        public async Task FinalizarPedidoParaPagamentoSemItensNotFounded()
        {
            var identificacao = Guid.NewGuid();

            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
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
            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
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
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");
            var produto = new Produto(CategoriaProduto.Lanche, "descricao", "Produto", 20);
            var produto2 = new Produto(CategoriaProduto.Bebida, "descricao", "Produto2", 10);
            produtoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(produto.Id))
                            .ReturnsAsync(produto);
            produtoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(produto2.Id))
                            .ReturnsAsync(produto2);

            var pedido = new Pedido(cliente);
            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);
            pedidoRepository.Setup(p => p.AtualizarPedidoAsync(It.IsAny<Pedido>()))
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
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");
            var produto = new Produto(CategoriaProduto.Lanche, "descricao", "Produto", 20);
            var produto2 = new Produto(CategoriaProduto.Bebida, "descricao", "Produto2", 10);
            produtoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(produto.Id))
                            .ReturnsAsync(produto);
            produtoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(produto2.Id))
                            .ReturnsAsync(produto2);

            var pedido = new Pedido(cliente);
            pedido.AdicionarItem(produto, 1, "");
            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);
            pedidoRepository.Setup(p => p.AtualizarPedidoAsync(It.IsAny<Pedido>()))
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
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");
            var produto = new Produto(CategoriaProduto.Lanche, "descricao", "Produto", 20);
            var produto2 = new Produto(CategoriaProduto.Bebida, "descricao", "Produto2", 10);
            produtoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(produto.Id))
                            .ReturnsAsync(produto);
            produtoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(produto2.Id))
                            .ReturnsAsync(produto2);

            var pedido = new Pedido(cliente);
            pedido.AdicionarItem(produto, 2, "remover");
            pedido.AdicionarItem(produto2, 1, "manter");

            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);
            pedidoRepository.Setup(p => p.AtualizarPedidoAsync(It.IsAny<Pedido>()))
                            .Callback<Pedido>((pedidoAtualizado) =>
                            {
                                Assert.Equal(1, pedidoAtualizado.Itens.Count);
                                Assert.Equal(2, pedidoAtualizado.Itens.First().Sequencial);
                                Assert.Equal("manter", pedidoAtualizado.Itens.First().Observacao);
                            })
                            .Returns(Task.CompletedTask);

            var pedidoItemAtualizado = new AtualizacaoPedidoItem
            {
                Sequencial = 2,
                IdentificacaoProduto = produto2.Id.ToString(),
                Quantidade = 1,
                Observacao = "manter",
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
            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
                            .ThrowsAsync(new Exception("Erro inesperado"));
            var identificacao = Guid.NewGuid().ToString();

            var resultado = await pedidoService.FinalizarPedidoParaPagamento(identificacao);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Contains($"Erro inesperado", resultado.Message);
        }

        [Fact]
        public async Task AtualizarPedidoAguardandoPagamentoThrowsException()
        {
            var cliente = new Cliente("Nome completo", "email@gmail.com", "582.202.320-72");

            var pedido = new Pedido(cliente);
            pedido.FecharPedidoParaPagamento();

            pedidoRepository.Setup(p => p.PesquisarPorIdentificacaoAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(pedido);

            var pedidoItemAtualizado = new AtualizacaoPedidoItem
            {
                Sequencial = 2,
                IdentificacaoProduto = Guid.NewGuid().ToString(),
                Quantidade = 1,
                Observacao = "",
            };

            var AtualizacaoPedido = new AtualizacaoPedido
            {
                Identificacao = pedido.Id.ToString(),
                PedidoItens = new List<AtualizacaoPedidoItem> { pedidoItemAtualizado }
            };

            var resultado = await pedidoService.AtualizarPedido(AtualizacaoPedido);

            Assert.False(resultado.IsSuccess());
            Assert.Null(resultado.Exception);
            Assert.Contains($"O pedido com identificação {pedido.Id.ToString()} não pode ser alterado pois está aguardando pagamento.", resultado.Message);
        }

    }
}
