using System;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Tests.Domain.Types;

public class StringNaoVaziaTest
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void StringNaoVazia_NaoPodeSerNuloOuVazio(string valor)
    {
        Assert.Throws<ArgumentException>(() => new StringNaoVazia(valor));
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var valor = "Valor";
        var stringNaoVazia = new StringNaoVazia(valor);

        var result = stringNaoVazia.ToString();

        Assert.Equal(valor, result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsStringToStringNaoVazia()
    {
        var valor = "Valor";

        StringNaoVazia stringNaoVazia = valor;

        Assert.Equal(valor, stringNaoVazia.Valor);
    }

    [Fact]
    public void ImplicitOperator_ConvertsStringNaoVaziaToString()
    {
        var valor = new StringNaoVazia("Valor");

        string result = valor;

        Assert.Equal("Valor", result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsNullStringToStringNaoVazia_ThrowsArgumentException()
    {
        string valor = null;

        Assert.Throws<ArgumentException>(() => {StringNaoVazia stringNaoVazia = valor; });
    }

    [Fact]
    public void Equals_ComparaDoisStringNaoVaziaIguais()
    {
        var stringNaoVazia = new StringNaoVazia("Valor");
        var stringNaoVazia2 = new StringNaoVazia("Valor");

        Assert.True(stringNaoVazia.Equals(stringNaoVazia2));
    }

    [Fact]
    public void Equals_ComparaDoisStringNaoVaziaDiferentes()
    {
        var stringNaoVazia = new StringNaoVazia("Valor");
        var stringNaoVazia2 = new StringNaoVazia("Valor2");

        Assert.False(stringNaoVazia.Equals(stringNaoVazia2));
    }
}