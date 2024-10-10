using System;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Tests.Domain.Types;

public class CpfValidoTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("1234567890")]
    [InlineData("12345678901")]
    [InlineData("123456789012")]
    [InlineData("1234567890123")]
    public void CpfValido_NaoPodeSerValorInvalido(string valor)
    {
        Assert.Throws<ArgumentException>(() => new CpfValido(valor));
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var valor = "58091454007";
        var cpfValido = new CpfValido(valor);

        var result = cpfValido.ToString();

        Assert.Equal(valor, result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsStringToCpfValido()
    {
        var valor = "58091454007";

        CpfValido cpfValido = valor;

        Assert.Equal(valor, cpfValido.ToString());
    }

    [Fact]
    public void ImplicitOperator_ConvertsCpfValidoToString()
    {
        var valor = new CpfValido("58091454007");

        string result = valor;

        Assert.Equal(valor.ToString(), result);
    }

    [Fact]
    public void Equals_ComparaDoisCpfValido()
    {
        var cpfValido1 = new CpfValido("58091454007");
        var cpfValido2 = new CpfValido("58091454007");

        Assert.True(cpfValido1.Equals(cpfValido2));
    }
}