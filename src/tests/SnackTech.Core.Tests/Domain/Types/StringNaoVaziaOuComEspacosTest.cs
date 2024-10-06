using System;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Tests.Domain.Types;

public class StringNaoVaziaOuComEspacosTest
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void StringNaoVaziaOuComEspacos_NaoPodeSerNuloOuVazio(string valor)
    {
        Assert.Throws<ArgumentException>(() => new StringNaoVaziaOuComEspacos(valor));
    }

    [Theory]
    [InlineData("           ")]
    [InlineData(" ")]
    public void StringNaoVaziaOuComEspacos_NaoPodeSerApenasEspacos(string valor)
    {
        Assert.Throws<ArgumentException>(() => new StringNaoVaziaOuComEspacos(valor));
    }

    [Fact]
    public void ToString_ReturnsValor()
    {
        var valor = "Valor";
        var stringNaoVaziaOuComEspacos = new StringNaoVaziaOuComEspacos(valor);

        var result = stringNaoVaziaOuComEspacos.ToString();

        Assert.Equal(valor, result);
    }

    [Fact]
    public void ToString_ReturnsValorWithSpaces()
    {
        var valor = "   Valor   ";
        var stringNaoVaziaOuComEspacos = new StringNaoVaziaOuComEspacos(valor);

        var result = stringNaoVaziaOuComEspacos.ToString();

        Assert.Equal(valor, result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsStringToStringNaoVaziaOuComEspacos()
    {
        var valor = "Valor";

        StringNaoVaziaOuComEspacos stringNaoVaziaOuComEspacos = valor;

        Assert.Equal(valor, stringNaoVaziaOuComEspacos.Valor);
    }


    [Fact]
    public void ImplicitOperator_ConvertsNullStringToStringNaoVaziaOuComEspacos_ThrowsArgumentException()
    {
        string valor = null;

         Assert.Throws<ArgumentException>(() => {StringNaoVaziaOuComEspacos stringNaoVaziaOuComEspacos = valor; });
    }


    [Fact]
    public void ImplicitOperator_ConvertsStringWithOnlySpacesToStringNaoVaziaOuComEspacos_ThrowsArgumentException()
    {
        string valor = "   ";

        Assert.Throws<ArgumentException>(() => {StringNaoVaziaOuComEspacos stringNaoVaziaOuComEspacos = valor; });
    }


    [Fact]
    public void ImplicitOperator_ConvertsStringNaoVaziaOuComEspacosToString()
    {
        var valor = new StringNaoVaziaOuComEspacos("Valor");

        string result = valor;

        Assert.Equal("Valor", result);
    }
}
