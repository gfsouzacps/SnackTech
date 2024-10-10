using System;
using FluentAssertions;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using SnackTech.Core.Gateways;
using Telerik.JustMock;

namespace SnackTech.Core.Tests.Gateways;

public class ProdutoGatewayTest
{
    private readonly IProdutoDataSource _dataSource;
    private readonly ProdutoGateway _produtoGateway;

    public ProdutoGatewayTest()
    {
        _dataSource = Mock.Create<IProdutoDataSource>();
        _produtoGateway = new ProdutoGateway(_dataSource);
    }

    [Fact]
    public void ConverterParaDto_DeveRetornarProdutoDto_ComDadosCorretos()
    {
        // Arrange
        var produto = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Bebida, "nome", "descricao", 10.99m);

        // Act
        var produtoDto = ProdutoGateway.ConverterParaDto(produto);

        // Assert
        produtoDto.Id.Should().Be(produto.Id.Valor);
        produtoDto.Categoria.Should().Be(produto.Categoria.Valor);
        produtoDto.Nome.Should().Be(produto.Nome.Valor);
        produtoDto.Descricao.Should().Be(produto.Descricao);
        produtoDto.Valor.Should().Be(produto.Valor.Valor);
    }

    [Fact]
    public void ConverterParaEntidade_DeveRetornarProduto_ComDadosCorretos()
    {
        // Arrange
        var produtoDto = new ProdutoDto { Id = Guid.NewGuid(), Categoria = CategoriaProdutoValido.Bebida, Nome = "nome", Descricao = "descricao", Valor = 10.99m };

        // Act
        var produto = ProdutoGateway.ConverterParaEntidade(produtoDto);

        // Assert
        produto.Id.Valor.Should().Be(produtoDto.Id);
        produto.Categoria.Valor.Should().Be(produtoDto.Categoria);
        produto.Nome.Valor.Should().Be(produtoDto.Nome);
        produto.Descricao.Should().Be(produtoDto.Descricao);
        produto.Valor.Valor.Should().Be(produtoDto.Valor);
    }

    [Fact]
    public async Task ProcurarProdutoPorNome_DeveChamarPesquisarPorNomeAsync_DoDataSource()
    {
        // Arrange
        var nome = new StringNaoVaziaOuComEspacos("nome");
    
        // Act
        await _produtoGateway.ProcurarProdutoPorNome(nome);
    
        // Assert
        Mock.Assert(() => _dataSource.PesquisarPorNomeAsync(Arg.IsAny<string>()), Occurs.Once());
    }
    
    [Fact]
    public async Task ProcurarProdutoPorNome_DeveRetornarProduto_SePesquisarPorNomeAsync_DoDataSource_RetornarProdutoDto()
    {
        // Arrange
        var nome = new StringNaoVaziaOuComEspacos("nome");
        var produtoDto = new ProdutoDto { Id = Guid.NewGuid(), Categoria = CategoriaProdutoValido.Bebida, Nome = "nome", Descricao = "descricao", Valor = 10.99m };
        Mock.Arrange(() => _dataSource.PesquisarPorNomeAsync(Arg.IsAny<string>())).ReturnsAsync(produtoDto);
    
        // Act
        var produto = await _produtoGateway.ProcurarProdutoPorNome(nome);
    
        // Assert
        produto.Should().NotBeNull();
        produto.Id.Valor.Should().Be(produtoDto.Id);
        produto.Categoria.Valor.Should().Be(produtoDto.Categoria);
        produto.Nome.Valor.Should().Be(produtoDto.Nome);
        produto.Descricao.Should().Be(produtoDto.Descricao);
        produto.Valor.Valor.Should().Be(produtoDto.Valor);
    }
    
    [Fact]
    public async Task ProcurarProdutoPorNome_DeveRetornarNull_SePesquisarPorNomeAsync_DoDataSource_RetornarNull()
    {
        // Arrange
        var nome = new StringNaoVaziaOuComEspacos("nome");
        Mock.Arrange(() => _dataSource.PesquisarPorNomeAsync(Arg.IsAny<string>())).ReturnsAsync((ProdutoDto?)null);
    
        // Act
        var produto = await _produtoGateway.ProcurarProdutoPorNome(nome);
    
        // Assert
        produto.Should().BeNull();
    }
    
    [Fact]
    public async Task ProcurarProdutoPorIdentificacao_DeveChamarPesquisarPorIdentificacaoAsync_DoDataSource()
    {
        // Arrange
        var identificacao = new GuidValido(Guid.NewGuid());
    
        // Act
        await _produtoGateway.ProcurarProdutoPorIdentificacao(identificacao);
    
        // Assert
        Mock.Assert(() => _dataSource.PesquisarPorIdentificacaoAsync(Arg.IsAny<Guid>()), Occurs.Once());
    }
    
    [Fact]
    public async Task ProcurarProdutoPorIdentificacao_DeveRetornarProduto_SePesquisarPorIdentificacaoAsync_DoDataSource_RetornarProdutoDto()
    {
        // Arrange
        var identificacao = new GuidValido(Guid.NewGuid());
        var produtoDto = new ProdutoDto { Id = Guid.NewGuid(), Categoria = CategoriaProdutoValido.Bebida, Nome = "nome", Descricao = "descricao", Valor = 10.99m };
        Mock.Arrange(() => _dataSource.PesquisarPorIdentificacaoAsync(Arg.IsAny<Guid>())).ReturnsAsync(produtoDto);
    
        // Act
        var produto = await _produtoGateway.ProcurarProdutoPorIdentificacao(identificacao);
    
        // Assert
        produto.Should().NotBeNull();
        produto.Id.Valor.Should().Be(produtoDto.Id);
        produto.Categoria.Valor.Should().Be(produtoDto.Categoria);
        produto.Nome.Valor.Should().Be(produtoDto.Nome);
        produto.Descricao.Should().Be(produtoDto.Descricao);
        produto.Valor.Valor.Should().Be(produtoDto.Valor);
    }
    
    [Fact]
    public async Task ProcurarProdutoPorIdentificacao_DeveRetornarNull_SePesquisarPorIdentificacaoAsync_DoDataSource_RetornarNull()
    {
        // Arrange
        var identificacao = new GuidValido(Guid.NewGuid());
        Mock.Arrange(() => _dataSource.PesquisarPorIdentificacaoAsync(Arg.IsAny<Guid>())).ReturnsAsync((ProdutoDto?)null);
    
        // Act
        var produto = await _produtoGateway.ProcurarProdutoPorIdentificacao(identificacao);
    
        // Assert
        produto.Should().BeNull();
    }

    [Fact]
    public async Task CadastrarNovoProduto_DeveChamarInserirProdutoAsync_DoDataSource()
    {
        // Arrange
        var produto = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Bebida, "nome", "descricao", 10.99m);
    
        // Act
        await _produtoGateway.CadastrarNovoProduto(produto);
    
        // Assert
        Mock.Assert(() => _dataSource.InserirProdutoAsync(Arg.IsAny<ProdutoDto>()), Occurs.Once());
    }
    
    [Fact]
    public async Task CadastrarNovoProduto_DeveRetornarTrue_SeInserirProdutoAsync_DoDataSource_RetornarTrue()
    {
        // Arrange
        var produto = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Bebida, "nome", "descricao", 10.99m);
        Mock.Arrange(() => _dataSource.InserirProdutoAsync(Arg.IsAny<ProdutoDto>())).ReturnsAsync(true);
    
        // Act
        var resultado = await _produtoGateway.CadastrarNovoProduto(produto);
    
        // Assert
        resultado.Should().BeTrue();
    }
    
    [Fact]
    public async Task CadastrarNovoProduto_DeveRetornarFalse_SeInserirProdutoAsync_DoDataSource_RetornarFalse()
    {
        // Arrange
        var produto = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Bebida, "nome", "descricao", 10.99m);
        Mock.Arrange(() => _dataSource.InserirProdutoAsync(Arg.IsAny<ProdutoDto>())).ReturnsAsync(false);
    
        // Act
        var resultado = await _produtoGateway.CadastrarNovoProduto(produto);
    
        // Assert
        resultado.Should().BeFalse();
    }
    
    [Fact]
    public async Task AtualizarProduto_DeveChamarAlterarProdutoAsync_DoDataSource()
    {
        // Arrange
        var produto = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Bebida, "nome", "descricao", 10.99m);
    
        // Act
        await _produtoGateway.AtualizarProduto(produto);
    
        // Assert
        Mock.Assert(() => _dataSource.AlterarProdutoAsync(Arg.IsAny<ProdutoDto>()), Occurs.Once());
    }
    
    [Fact]
    public async Task AtualizarProduto_DeveRetornarTrue_SeAlterarProdutoAsync_DoDataSource_RetornarTrue()
    {
        // Arrange
        var produto = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Bebida, "nome", "descricao", 10.99m);
        Mock.Arrange(() => _dataSource.AlterarProdutoAsync(Arg.IsAny<ProdutoDto>())).ReturnsAsync(true);
    
        // Act
        var resultado = await _produtoGateway.AtualizarProduto(produto);
    
        // Assert
        resultado.Should().BeTrue();
    }
    
    [Fact]
    public async Task AtualizarProduto_DeveRetornarFalse_SeAlterarProdutoAsync_DoDataSource_RetornarFalse()
    {
        // Arrange
        var produto = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Bebida, "nome", "descricao", 10.99m);
        Mock.Arrange(() => _dataSource.AlterarProdutoAsync(Arg.IsAny<ProdutoDto>())).ReturnsAsync(false);
    
        // Act
        var resultado = await _produtoGateway.AtualizarProduto(produto);
    
        // Assert
        resultado.Should().BeFalse();
    }

    [Fact]
    public async Task RemoverProduto_DeveChamarRemoverProdutoPorIdentificacaoAsync_DoDataSource()
    {
        // Arrange
        var id = Guid.NewGuid();
    
        // Act
        await _produtoGateway.RemoverProduto(id);
    
        // Assert
        Mock.Assert(() => _dataSource.RemoverProdutoPorIdentificacaoAsync(Arg.IsAny<Guid>()), Occurs.Once());
    }
    
    [Fact]
    public async Task RemoverProduto_DeveRetornarTrue_SeRemoverProdutoPorIdentificacaoAsync_DoDataSource_RetornarTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        Mock.Arrange(() => _dataSource.RemoverProdutoPorIdentificacaoAsync(Arg.IsAny<Guid>())).ReturnsAsync(true);
    
        // Act
        var resultado = await _produtoGateway.RemoverProduto(id);
    
        // Assert
        resultado.Should().BeTrue();
    }
    
    [Fact]
    public async Task RemoverProduto_DeveRetornarFalse_SeRemoverProdutoPorIdentificacaoAsync_DoDataSource_RetornarFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        Mock.Arrange(() => _dataSource.RemoverProdutoPorIdentificacaoAsync(Arg.IsAny<Guid>())).ReturnsAsync(false);
    
        // Act
        var resultado = await _produtoGateway.RemoverProduto(id);
    
        // Assert
        resultado.Should().BeFalse();
    }
    
    [Fact]
    public async Task ProcurarProdutosPorCategoria_DeveChamarPesquisarPorCategoriaIdAsync_DoDataSource()
    {
        // Arrange
        var categoria = CategoriaProdutoValido.Acompanhamento;
    
        // Act
        await _produtoGateway.ProcurarProdutosPorCategoria(categoria);
    
        // Assert
        Mock.Assert(() => _dataSource.PesquisarPorCategoriaIdAsync(Arg.IsAny<int>()), Occurs.Once());
    }
    
    [Fact]
    public async Task ProcurarProdutosPorCategoria_DeveRetornarProdutos_SePesquisarPorCategoriaIdAsync_DoDataSource_RetornarProdutos()
    {
        // Arrange
        var categoria = CategoriaProdutoValido.Acompanhamento;
        var produtosDto = new List<ProdutoDto>
        {
            new ProdutoDto{
                Id = Guid.NewGuid(),
                Categoria = categoria,
                Nome = "nome1",
                Descricao = "descricao1",
                Valor = 10.99m
            },
            new ProdutoDto{
                Id = Guid.NewGuid(),
                Categoria = categoria,
                Nome = "nome2",
                Descricao = "descricao2",
                Valor = 10.99m
            }
        };
        Mock.Arrange(() => _dataSource.PesquisarPorCategoriaIdAsync(Arg.IsAny<int>())).ReturnsAsync(produtosDto);
    
        // Act
        var resultado = await _produtoGateway.ProcurarProdutosPorCategoria(categoria);
    
        // Assert
        resultado.Should().HaveCount(2);
        resultado.Should().Contain(p => p.Categoria == categoria);
    }
    
    [Fact]
    public async Task ProcurarProdutosPorCategoria_DeveRetornarProdutosVazios_SePesquisarPorCategoriaIdAsync_DoDataSource_RetornarProdutosVazios()
    {
        // Arrange
        var categoria = CategoriaProdutoValido.Acompanhamento;
        Mock.Arrange(() => _dataSource.PesquisarPorCategoriaIdAsync(Arg.IsAny<int>())).ReturnsAsync(new List<ProdutoDto>());
    
        // Act
        var resultado = await _produtoGateway.ProcurarProdutosPorCategoria(categoria);
    
        // Assert
        resultado.Should().BeEmpty();
    }
}
