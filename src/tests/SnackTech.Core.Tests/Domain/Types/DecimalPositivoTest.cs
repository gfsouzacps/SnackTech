using System;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Tests.Domain.Types;

public class DecimalPositivoTest
{
    [Theory]
    [InlineData(-1)]
    [InlineData(-99.09)]
    public void DecimalPositivo_NaoPodeSerValorInvalido(decimal valor)
    {
        Assert.Throws<ArgumentException>(() => new DecimalPositivo(valor));
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var valor = 1.5m;
        var decimalPositivo = new DecimalPositivo(valor);

        var result = decimalPositivo.ToString();

        Assert.Equal(valor.ToString(), result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsDecimalToDecimalPositivo()
    {
        var valor = 1.5m;

        DecimalPositivo decimalPositivo = valor;

        Assert.Equal(valor, decimalPositivo.Valor);
    }

    [Fact]
    public void ImplicitOperator_ConvertsDecimalPositivoToDecimal()
    {
        var valor = new DecimalPositivo(1.5m);

        decimal result = valor;

        Assert.Equal(1.5m, result);
    }

    [Fact]
    public void Equals_ComparaDoisDecimalPositivo()
    {
        var decimalPositivo1 = new DecimalPositivo(1.5m);
        var decimalPositivo2 = new DecimalPositivo(1.5m);

        Assert.True(decimalPositivo1.Equals(decimalPositivo2));
    }

    [Fact]
    public void Equals_ComparaDecimalPositivoComOutroTipo()
    {
        var decimalPositivo = new DecimalPositivo(1.5m);
        var outroTipo = "outro tipo";

        Assert.False(decimalPositivo.Equals(outroTipo));
    }
}