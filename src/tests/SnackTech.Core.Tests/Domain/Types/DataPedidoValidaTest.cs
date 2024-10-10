using System;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Tests.Domain.Types;

public class DataPedidoValidaTest
{
    [Theory]
    [InlineData(2023, 12, 31)]
    [InlineData(1999, 04, 05)]
    [InlineData(0001, 01, 01)]
    public void DataPedidoValida_NaoPodeSerDataAnteriorAMinima(int year, int month, int day)
    {
        var valor = new DateTime(year, month, day);

        var exception = Assert.Throws<ArgumentException>(() => new DataPedidoValida(valor));

        Assert.Equal("Não é permitido criar pedidos com data anterior a 01/01/2024.", exception.Message);
    }

    public void DataPedidoValida_NaoPodeSerDataFutura()
    {
        var valor = DateTime.Now.AddDays(1);

        var exception = Assert.Throws<ArgumentException>(() => new DataPedidoValida(valor));

        Assert.Equal("Não é permitido criar pedidos com data posterior a data atual.", exception.Message);
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var valor = new DateTime(2024, 1, 1, 12, 0, 0);
        var dataPedidoValida = new DataPedidoValida(valor);

        var result = dataPedidoValida.ToString();

        Assert.Equal(valor.ToString(), result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsDateTimeToDataPedidoValida()
    {
        var valor = new DateTime(2024, 1, 1, 12, 0, 0);

        DataPedidoValida dataPedidoValida = valor;

        Assert.Equal(valor, dataPedidoValida.Valor);
    }

    [Fact]
    public void ImplicitOperator_ConvertsDataPedidoValidaToDateTime()
    {
        var valor = new DateTime(2024, 1, 1, 12, 0, 0);
        var dataPedidoValida = new DataPedidoValida(valor);

        DateTime result = dataPedidoValida;

        Assert.Equal(valor, result);
    }

    [Fact]
    public void Equals_ComparaDoisDataPedidoValida()
    {
        var dataPedidoValida1 = new DataPedidoValida(new DateTime(2024, 1, 1, 12, 0, 0));
        var dataPedidoValida2 = new DataPedidoValida(new DateTime(2024, 1, 1, 12, 0, 0));

        Assert.True(dataPedidoValida1.Equals(dataPedidoValida2));
    }

    [Fact]
    public void Equals_ComparaDoisDataPedidoValidaDiferente()
    {
        var dataPedidoValida1 = new DataPedidoValida(new DateTime(2024, 1, 1, 12, 0, 0));
        var dataPedidoValida2 = new DataPedidoValida(new DateTime(2024, 1, 1, 12, 0, 1));

        Assert.False(dataPedidoValida1.Equals(dataPedidoValida2));
    }

    [Fact]
    public void Equals_ComparaDataPedidoValidaComOutroTipo()
    {
        var dataPedidoValida = new DataPedidoValida(new DateTime(2024, 1, 1, 12, 0, 0));
        var outroTipo = "outro tipo";

        Assert.False(dataPedidoValida.Equals(outroTipo));
    }
}