using System;
using FluentAssertions;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using SnackTech.Core.Gateways;
using Telerik.JustMock;

namespace SnackTech.Core.Tests.Gateways;

public class PedidoGatewayTest
{
    private readonly IPedidoDataSource _dataSource;
    private readonly PedidoGateway _pedidoGateway;
    private PedidoDto _pedidoDtoExemplo;
    private Pedido _pedidoExemplo;

    public PedidoGatewayTest()
    {
        _dataSource = Mock.Create<IPedidoDataSource>();
        _pedidoGateway = new PedidoGateway(_dataSource);

        _pedidoDtoExemplo = new PedidoDto
        {
            Id = Guid.NewGuid(),
            DataCriacao = DateTime.Now,
            Status = StatusPedidoValido.AguardandoPagamento,
            Cliente = new ClienteDto
            {
                Id = Guid.NewGuid(),
                Nome = "nome",
                Email = "email@email.com",
                Cpf = "00000000191"
            },
            Itens = new List<PedidoItemDto>
            {
                new PedidoItemDto
                {
                    Id = Guid.NewGuid(),
                    Quantidade = 2,
                    Observacao = "observacao",
                    Valor = 21.98m,
                    Produto = new ProdutoDto
                    {
                        Id = Guid.NewGuid(),
                        Categoria = CategoriaProdutoValido.Acompanhamento,
                        Nome = "nome",
                        Descricao = "descricao",
                        Valor = 10.99m
                    }
                }
            }
        };

        _pedidoExemplo = new Pedido(
            Guid.NewGuid(),
            DateTime.Now,
            StatusPedidoValido.Iniciado,
            new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191"),
            new List<PedidoItem>
            {
                new PedidoItem(
                    Guid.NewGuid(),
                    new Produto(Guid.NewGuid(), CategoriaProdutoValido.Acompanhamento, "nome", "descricao", 10.99m),
                    2,
                    "observacao"
                )
            }
        );
    }

    [Fact]
    public void ConverterParaEntidade_DeveRetornarPedido_ComDadosCorretos()
    {
        // Act
        var pedido = PedidoGateway.ConverterParaEntidade(_pedidoDtoExemplo);
    
        // Assert
        pedido.Id.Valor.Should().Be(_pedidoDtoExemplo.Id);
        pedido.DataCriacao.Valor.Should().Be(_pedidoDtoExemplo.DataCriacao);
        pedido.Status.Valor.Should().Be(_pedidoDtoExemplo.Status);
        pedido.Cliente.Nome.Valor.Should().Be(_pedidoDtoExemplo.Cliente.Nome);
        pedido.Cliente.Email.Valor.Should().Be(_pedidoDtoExemplo.Cliente.Email);
        pedido.Cliente.Cpf.Valor.Should().Be(_pedidoDtoExemplo.Cliente.Cpf);
        pedido.Itens.Should().HaveCount(1);
        pedido.Itens[0].Id.Valor.Should().Be(_pedidoDtoExemplo.Itens.First().Id);
        pedido.Itens[0].Quantidade.Valor.Should().Be(_pedidoDtoExemplo.Itens.First().Quantidade);
        pedido.Itens[0].Observacao.Should().Be(_pedidoDtoExemplo.Itens.First().Observacao);
        pedido.Itens[0].Produto.Id.Valor.Should().Be(_pedidoDtoExemplo.Itens.First().Produto.Id);
        pedido.Itens[0].Produto.Categoria.Valor.Should().Be(_pedidoDtoExemplo.Itens.First().Produto.Categoria);
        pedido.Itens[0].Produto.Descricao.Should().Be(_pedidoDtoExemplo.Itens.First().Produto.Descricao);
        pedido.Itens[0].Produto.Valor.Valor.Should().Be(_pedidoDtoExemplo.Itens.First().Produto.Valor);
        pedido.Itens[0].Valor().Valor.Should().Be(_pedidoDtoExemplo.Itens.First().Valor);
    }
    
    [Fact]
    public void ConverterParaDto_DeveRetornarPedidoDto_ComDadosCorretos()
    {
        // Act
        var pedidoDto = PedidoGateway.ConverterParaDto(_pedidoExemplo);
    
        // Assert
        pedidoDto.Id.Should().Be(_pedidoExemplo.Id.Valor);
        pedidoDto.DataCriacao.Should().Be(_pedidoExemplo.DataCriacao.Valor);
        pedidoDto.Status.Should().Be(_pedidoExemplo.Status.Valor);
        pedidoDto.Cliente.Id.Should().Be(_pedidoExemplo.Cliente.Id.Valor);
        pedidoDto.Cliente.Nome.Should().Be(_pedidoExemplo.Cliente.Nome.Valor);
        pedidoDto.Cliente.Email.Should().Be(_pedidoExemplo.Cliente.Email);
        pedidoDto.Cliente.Cpf.Should().Be(_pedidoExemplo.Cliente.Cpf);
        pedidoDto.Itens.Should().HaveCount(1);
        pedidoDto.Itens.First().Id.Should().Be(_pedidoExemplo.Itens.First().Id.Valor);
        pedidoDto.Itens.First().Quantidade.Should().Be(_pedidoExemplo.Itens.First().Quantidade);
        pedidoDto.Itens.First().Observacao.Should().Be(_pedidoExemplo.Itens.First().Observacao);
        pedidoDto.Itens.First().Produto.Id.Should().Be(_pedidoExemplo.Itens.First().Produto.Id.Valor);
        pedidoDto.Itens.First().Produto.Categoria.Should().Be(_pedidoExemplo.Itens.First().Produto.Categoria.Valor);
        pedidoDto.Itens.First().Produto.Descricao.Should().Be(_pedidoExemplo.Itens.First().Produto.Descricao);
        pedidoDto.Itens.First().Produto.Valor.Should().Be(_pedidoExemplo.Itens.First().Produto.Valor);
        pedidoDto.Itens.First().Valor.Should().Be(_pedidoExemplo.Itens.First().Valor());
    }

    [Fact]
    public async Task CadastrarNovoPedido_DeveChamarInserirPedidoAsync_DoDataSource()
    {
        // Act
        await _pedidoGateway.CadastrarNovoPedido(_pedidoExemplo);
    
        // Assert
       Mock.Assert(() => _dataSource.InserirPedidoAsync(Arg.IsAny<PedidoDto>()), Occurs.Once());
    }
    
    [Fact]
    public async Task CadastrarNovoPedido_DeveRetornarTrue_SeInserirPedidoAsync_DoDataSource_RetornarTrue()
    {
        // Arrange
        Mock.Arrange(() => _dataSource.InserirPedidoAsync(Arg.IsAny<PedidoDto>())).ReturnsAsync(true);
    
        // Act
        var result = await _pedidoGateway.CadastrarNovoPedido(_pedidoExemplo);
    
        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task CadastrarNovoPedido_DeveRetornarFalse_SeInserirPedidoAsync_DoDataSource_RetornarFalse()
    {
        // Arrange
        Mock.Arrange(() => _dataSource.InserirPedidoAsync(Arg.IsAny<PedidoDto>())).ReturnsAsync(false);
    
        // Act
        var result = await _pedidoGateway.CadastrarNovoPedido(_pedidoExemplo);
    
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public async Task PesquisarPorIdentificacao_DeveChamarPesquisarPorIdentificacaoAsync_DoDataSource()
    {
        // Act
        await _pedidoGateway.PesquisarPorIdentificacao(_pedidoDtoExemplo.Id);
    
        // Assert
        Mock.Assert(() => _dataSource.PesquisarPorIdentificacaoAsync(Arg.IsAny<Guid>()), Occurs.Once());
    }
    
    [Fact]
    public async Task PesquisarPorIdentificacao_DeveRetornarPedido_SePesquisarPorIdentificacaoAsync_DoDataSource_RetornarPedidoDto()
    {
        // Arrange
        Mock.Arrange(() => _dataSource.PesquisarPorIdentificacaoAsync(Arg.IsAny<Guid>())).ReturnsAsync(_pedidoDtoExemplo);
    
        // Act
        var pedido = await _pedidoGateway.PesquisarPorIdentificacao(_pedidoDtoExemplo.Id);
    
        // Assert
        pedido.Should().NotBeNull();
        pedido.Id.Valor.Should().Be(_pedidoDtoExemplo.Id);
        pedido.DataCriacao.Valor.Should().Be(_pedidoDtoExemplo.DataCriacao);
        pedido.Status.Valor.Should().Be(_pedidoDtoExemplo.Status);
        pedido.Cliente.Nome.Valor.Should().Be(_pedidoDtoExemplo.Cliente.Nome);
        pedido.Cliente.Email.Valor.Should().Be(_pedidoDtoExemplo.Cliente.Email);
        pedido.Cliente.Cpf.Valor.Should().Be(_pedidoDtoExemplo.Cliente.Cpf);
        pedido.Itens.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task PesquisarPorIdentificacao_DeveRetornarNull_SePesquisarPorIdentificacaoAsync_DoDataSource_RetornarNull()
    {
        // Arrange
        Mock.Arrange(() => _dataSource.PesquisarPorIdentificacaoAsync(Arg.IsAny<Guid>())).ReturnsAsync((PedidoDto?)null);
    
        // Act
        var pedido = await _pedidoGateway.PesquisarPorIdentificacao(_pedidoDtoExemplo.Id);
    
        // Assert
        pedido.Should().BeNull();
    }

    [Fact]
    public async Task PesquisarPedidosPorCliente_DeveChamarPesquisarPedidosPorClienteIdAsync_DoDataSource()
    {
        // Act
        await _pedidoGateway.PesquisarPedidosPorCliente(_pedidoDtoExemplo.Cliente.Id);
    
        // Assert
        Mock.Assert(() => _dataSource.PesquisarPedidosPorClienteIdAsync(Arg.IsAny<Guid>()), Occurs.Once());
    }
    
    [Fact]
    public async Task PesquisarPedidosPorCliente_DeveRetornarPedidos_ComDadosCorretos()
    {
        // Arrange
        var pedidosDto = new List<PedidoDto>
        {
            _pedidoDtoExemplo,
            new PedidoDto
            {
                Id = Guid.NewGuid(),
                DataCriacao = DateTime.Now,
                Status = StatusPedidoValido.AguardandoPagamento,
                Cliente = _pedidoDtoExemplo.Cliente,
                Itens = new List<PedidoItemDto>()
            }
        };
    
        Mock.Arrange(() => _dataSource.PesquisarPedidosPorClienteIdAsync(Arg.IsAny<Guid>())).ReturnsAsync(pedidosDto);
    
        // Act
        var pedidos = await _pedidoGateway.PesquisarPedidosPorCliente(_pedidoDtoExemplo.Cliente.Id);
    
        // Assert
        pedidos.Should().HaveCount(2);
        pedidos.Should().Contain(p => p.Id == _pedidoDtoExemplo.Id);
        pedidos.Should().Contain(p => p.Cliente.Id == _pedidoDtoExemplo.Cliente.Id);
    }
    
    [Fact]
    public async Task PesquisarPedidosPorCliente_DeveRetornarListaVazia_SePesquisarPedidosPorClienteIdAsync_DoDataSource_RetornarListaVazia()
    {
        // Arrange
        Mock.Arrange(() => _dataSource.PesquisarPedidosPorClienteIdAsync(Arg.IsAny<Guid>())).ReturnsAsync(new List<PedidoDto>());
    
        // Act
        var pedidos = await _pedidoGateway.PesquisarPedidosPorCliente(_pedidoDtoExemplo.Cliente.Id);
    
        // Assert
        pedidos.Should().BeEmpty();
    }
    
    [Fact]
    public async Task PesquisarPedidosPorStatus_DeveChamarPesquisarPedidosPorStatusAsync_DoDataSource()
    {
        // Act
        await _pedidoGateway.PesquisarPedidosPorStatus(StatusPedidoValido.AguardandoPagamento);
    
        // Assert
        Mock.Assert(() => _dataSource.PesquisarPedidosPorStatusAsync(Arg.IsAny<int>()), Occurs.Once());
    }
    
    [Fact]
    public async Task PesquisarPedidosPorStatus_DeveRetornarPedidos_ComDadosCorretos()
    {
        // Arrange
        var pedidosDto = new List<PedidoDto>
        {
            _pedidoDtoExemplo,
            new PedidoDto
            {
                Id = Guid.NewGuid(),
                DataCriacao = DateTime.Now,
                Status = StatusPedidoValido.AguardandoPagamento,
                Cliente = _pedidoDtoExemplo.Cliente,
                Itens = new List<PedidoItemDto>()
            }
        };
    
        Mock.Arrange(() => _dataSource.PesquisarPedidosPorStatusAsync(Arg.IsAny<int>())).ReturnsAsync(pedidosDto);
    
        // Act
        var pedidos = await _pedidoGateway.PesquisarPedidosPorStatus(StatusPedidoValido.AguardandoPagamento);
    
        // Assert
        pedidos.Should().HaveCount(2);
        pedidos.Should().Contain(p => p.Id == _pedidoDtoExemplo.Id);
        pedidos.Should().Contain(p => p.Status == StatusPedidoValido.AguardandoPagamento);
    }
    
    [Fact]
    public async Task PesquisarPedidosPorStatus_DeveRetornarListaVazia_SePesquisarPedidosPorStatusAsync_DoDataSource_RetornarListaVazia()
    {
        // Arrange
        Mock.Arrange(() => _dataSource.PesquisarPedidosPorStatusAsync(Arg.IsAny<int>())).ReturnsAsync(new List<PedidoDto>());
    
        // Act
        var pedidos = await _pedidoGateway.PesquisarPedidosPorStatus(StatusPedidoValido.AguardandoPagamento);
    
        // Assert
        pedidos.Should().BeEmpty();
    }

    [Fact]
    public async Task AtualizarStatusPedido_DeveChamarAtualizarStatusPedidoAsync_DoDataSource()
    {
        // Act
        await _pedidoGateway.AtualizarStatusPedido(_pedidoExemplo);
    
        // Assert
        Mock.Assert(() => _dataSource.AtualizarStatusPedidoAsync(Arg.IsAny<PedidoDto>()), Occurs.Once());
    }
    
    [Fact]
    public async Task AtualizarStatusPedido_DeveRetornarTrue_SeAtualizarStatusPedidoAsync_DoDataSource_RetornarTrue()
    {
        // Arrange
        Mock.Arrange(() => _dataSource.AtualizarStatusPedidoAsync(Arg.IsAny<PedidoDto>())).ReturnsAsync(true);
    
        // Act
        var resultado = await _pedidoGateway.AtualizarStatusPedido(_pedidoExemplo);
    
        // Assert
        resultado.Should().BeTrue();
    }
    
    [Fact]
    public async Task AtualizarStatusPedido_DeveRetornarFalse_SeAlterarPedidoAsync_DoDataSource_RetornarFalse()
    {
        // Arrange
        Mock.Arrange(() => _dataSource.AlterarPedidoAsync(Arg.IsAny<PedidoDto>())).ReturnsAsync(false);
    
        // Act
        var resultado = await _pedidoGateway.AtualizarStatusPedido(_pedidoExemplo);
    
        // Assert
        resultado.Should().BeFalse();
    }
    
    [Fact]
    public async Task AtualizarItensDoPedido_DeveChamarAlterarItensDoPedidoAsync_DoDataSource()
    {
        // Act
        await _pedidoGateway.AtualizarItensDoPedido(_pedidoExemplo);
    
        // Assert
        Mock.Assert(() => _dataSource.AlterarItensDoPedidoAsync(Arg.IsAny<PedidoDto>()), Occurs.Once());
    }
    
    [Fact]
    public async Task AtualizarItensDoPedido_DeveRetornarTrue_SeAlterarItensDoPedidoAsync_DoDataSource_RetornarTrue()
    {
        // Arrange
        Mock.Arrange(() => _dataSource.AlterarItensDoPedidoAsync(Arg.IsAny<PedidoDto>())).ReturnsAsync(true);
    
        // Act
        var resultado = await _pedidoGateway.AtualizarItensDoPedido(_pedidoExemplo);
    
        // Assert
        resultado.Should().BeTrue();
    }
    
    [Fact]
    public async Task AtualizarItensDoPedido_DeveRetornarFalse_SeAlterarItensDoPedidoAsync_DoDataSource_RetornarFalse()
    {
        // Arrange
        Mock.Arrange(() => _dataSource.AlterarItensDoPedidoAsync(Arg.IsAny<PedidoDto>())).ReturnsAsync(false);
    
        // Act
        var resultado = await _pedidoGateway.AtualizarItensDoPedido(_pedidoExemplo);
    
        // Assert
        resultado.Should().BeFalse();
    }
}
