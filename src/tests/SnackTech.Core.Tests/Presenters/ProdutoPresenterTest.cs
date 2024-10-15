using System;
using FluentAssertions;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using SnackTech.Core.Presenters;

namespace SnackTech.Core.Tests.Presenters;

public class ProdutoPresenterTest
{
    private readonly Produto _produtoExemplo;

    public ProdutoPresenterTest()
    {
        _produtoExemplo = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Acompanhamento, "nome", "descricao", 9.99m);
    }

    [Fact]
    public void ConverterParaDto_DeveRetornarProdutoDtoComDadosCorretos()
    {
        // Act
        var produtoDto = ProdutoPresenter.ConverterParaDto(_produtoExemplo);
    
        // Assert
        produtoDto.Should().NotBeNull();
        produtoDto.IdentificacaoProduto.Should().Be(_produtoExemplo.Id.Valor);
        produtoDto.Categoria.Should().Be(_produtoExemplo.Categoria.Valor);
        produtoDto.Descricao.Should().Be(_produtoExemplo.Descricao);
        produtoDto.Nome.Should().Be(_produtoExemplo.Nome.Valor);
        produtoDto.Valor.Should().Be(_produtoExemplo.Valor.Valor);
    }
    
    [Fact]
    public void ApresentarResultadoProduto_DeveRetornarResultadoOperacaoComProdutoDto()
    {
        // Act
        var resultado = ProdutoPresenter.ApresentarResultadoProduto(_produtoExemplo);
    
        // Assert
        resultado.Should().NotBeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.IdentificacaoProduto.Should().Be(_produtoExemplo.Id.Valor);
        resultado.Dados.Categoria.Should().Be(_produtoExemplo.Categoria.Valor);
        resultado.Dados.Descricao.Should().Be(_produtoExemplo.Descricao);
        resultado.Dados.Nome.Should().Be(_produtoExemplo.Nome.Valor);
        resultado.Dados.Valor.Should().Be(_produtoExemplo.Valor.Valor);
    }
    
    [Fact]
    public void ApresentarResultadoListaProdutos_DeveRetornarResultadoOperacaoComListaProdutoDto()
    {
        // Arrange
        var produtos = new List<Produto>
        {
            _produtoExemplo,
            new Produto(Guid.NewGuid(), CategoriaProdutoValido.Bebida, "Nome 2", "Descrição 2", 20.99m)
        };
    
        // Act
        var resultado = ProdutoPresenter.ApresentarResultadoListaProdutos(produtos);
    
        // Assert
        resultado.Should().NotBeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.Count().Should().Be(2);
    }
}
