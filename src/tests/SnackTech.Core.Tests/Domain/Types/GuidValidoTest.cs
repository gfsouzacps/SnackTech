using System;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Tests.Domain.Types;

public class GuidValidoTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid-guid")]
    public void GuidValido_NaoPodeSerValorInvalido(string valor)
    {
        Assert.Throws<ArgumentException>(() => new GuidValido(valor));
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var valor = Guid.NewGuid().ToString();
        var guidValido = new GuidValido(valor);

        var result = guidValido.ToString();

        Assert.Equal(valor, result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsStringToGuidValido()
    {
        var valor = Guid.NewGuid().ToString();

        GuidValido guidValido = valor;
        GuidValido expected = new GuidValido(valor);

        Assert.Equal(expected, guidValido);
    }

    [Fact]
    public void ImplicitOperator_ConvertsGuidValidoToString()
    {
        var valor = Guid.NewGuid().ToString();
        var guidValido = new GuidValido(valor);

        string result = guidValido;

        Assert.Equal(valor, result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsGuidValidoToGuid()
    {
        var valor = Guid.NewGuid();
        var guidValido = new GuidValido(valor);

        Guid result = guidValido;

        Assert.Equal(valor, result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsGuidToGuidValido()
    {
        var valor = Guid.NewGuid();
        GuidValido guidValido = valor;

        Assert.Equal(valor, guidValido.Valor);
    }

    [Fact]
    public void Equals_ComparaDoisGuidValido()
    {
        var guidValido1 = new GuidValido(Guid.NewGuid());
        var guidValido2 = new GuidValido(guidValido1.Valor);

        Assert.True(guidValido1.Equals(guidValido2));
    }

    [Fact]
    public void Equals_ComparaGuidValidoComOutroTipo()
    {
        var guidValido = new GuidValido(Guid.NewGuid().ToString());
        var outroTipo = "outro tipo";

        Assert.False(outroTipo.Equals(guidValido));
    }
}