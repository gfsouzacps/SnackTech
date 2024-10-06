using System;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Tests.Domain.Types;

public class InteiroPositivoTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void InteiroPositivo_NaoPodeSerValorInvalido(int valor)
    {
        Assert.Throws<ArgumentException>(() => new InteiroPositivo(valor));
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var valor = 1;
        var inteiroPositivo = new InteiroPositivo(valor);

        var result = inteiroPositivo.ToString();

        Assert.Equal("1", result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsIntToInteiroPositivo()
    {
        var valor = 1;

        InteiroPositivo inteiroPositivo = valor;
        var inteiroPositivoEsperado = new InteiroPositivo(1);

        Assert.Equal(inteiroPositivoEsperado, inteiroPositivo);
    }

    [Fact]
    public void ImplicitOperator_ConvertsInteiroPositivoToInt()
    {
        var valor = new InteiroPositivo(1);

        int result = valor;

        Assert.Equal(1, result);
    }

    [Fact]
    public void Equals_ComparaDoisInteiroPositivo()
    {
        var inteiroPositivo1 = new InteiroPositivo(1);
        var inteiroPositivo2 = new InteiroPositivo(1);

        Assert.True(inteiroPositivo1.Equals(inteiroPositivo2));
    }

    [Fact]
    public void Equals_ComparaInteiroPositivoComOutroTipo()
    {
        var inteiroPositivo = new InteiroPositivo(1);
        var outroTipo = "outro tipo";

        Assert.False(inteiroPositivo.Equals(outroTipo));
    }
}