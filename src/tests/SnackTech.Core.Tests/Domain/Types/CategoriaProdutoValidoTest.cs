using System;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Tests.Domain.Types;

public class CategoriaProdutoValidoTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(99)]
    public void CategoriaProdutoValido_NaoPodeSerValorInvalido(int valor)
    {
        Assert.Throws<ArgumentException>(() => new CategoriaProdutoValido(valor));
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var valor = 1;
        var categoriaProdutoValido = new CategoriaProdutoValido(valor);

        var result = categoriaProdutoValido.ToString();

        Assert.Equal(valor.ToString(), result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsIntToCategoriaProdutoValido()
    {
        var valor = 1;

        CategoriaProdutoValido categoriaProdutoValido = valor;

        Assert.Equal(valor, categoriaProdutoValido.Valor);
    }

    [Fact]
    public void ImplicitOperator_ConvertsCategoriaProdutoValidoToInt()
    {
        var valor = new CategoriaProdutoValido(1);

        int result = valor;

        Assert.Equal(valor.Valor, result);
    }

    [Fact]
    public void Equals_ComparaDoisCategoriaProdutoValido()
    {
        var categoriaProdutoValido1 = new CategoriaProdutoValido(1);
        var categoriaProdutoValido2 = new CategoriaProdutoValido(1);

        Assert.True(categoriaProdutoValido1.Equals(categoriaProdutoValido2));
    }

    [Fact]
    public void Equals_ComparaCategoriaProdutoValidoComOutroTipo()
    {
        var categoriaProdutoValido = new CategoriaProdutoValido(1);
        var outroTipo = "outro tipo";

        Assert.False(categoriaProdutoValido.Equals(outroTipo));
    }
}