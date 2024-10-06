using System;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Tests.Domain.Types;

public class StatusPedidoValidoTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(99)]
    public void StatusPedidoValido_NaoPodeSerValorInvalido(int valor)
    {
        Assert.Throws<ArgumentException>(() => new StatusPedidoValido(valor));
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var valor = 1;
        var statusPedidoValido = new StatusPedidoValido(valor);

        var result = statusPedidoValido.ToString();

        Assert.Equal("1", result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void ImplicitOperator_ConvertsIntToStatusPedidoValido(int valor)
    {
        var statusPedidoValidoEsperado = new StatusPedidoValido(valor);
        StatusPedidoValido statusPedidoValido = valor;
        Assert.Equal(statusPedidoValidoEsperado, statusPedidoValido);
    }

    [Fact]
    public void ImplicitOperator_ConvertsStatusPedidoValidoToInt()
    {
        var valor = new StatusPedidoValido(1);

        int result = valor;

        Assert.Equal(1, result);
    }

    [Fact]
    public void Equals_ComparaDoisStatusPedidoValido()
    {
        var statusPedidoValido1 = new StatusPedidoValido(1);
        var statusPedidoValido2 = new StatusPedidoValido(1);

        Assert.True(statusPedidoValido1.Equals(statusPedidoValido2));
    }

    [Fact]
    public void Equals_ComparaStatusPedidoValidoComOutroTipo()
    {
        var statusPedidoValido = new StatusPedidoValido(1);
        var outroTipo = "outro tipo";

        Assert.False(statusPedidoValido.Equals(outroTipo));
    }
}