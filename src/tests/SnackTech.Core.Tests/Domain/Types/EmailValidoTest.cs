using System;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Tests.Domain.Types;

public class EmailValidoTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("invalid-email")]
    public void EmailValido_NaoPodeSerValorInvalido(string valor)
    {
        Assert.Throws<ArgumentException>(() => new EmailValido(valor));
    }

    [Fact]
    public void ToString_ReturnsValue()
    {
        var valor = "email@example.com";
        var emailValido = new EmailValido(valor);

        var result = emailValido.ToString();

        Assert.Equal(valor, result);
    }

    [Fact]
    public void ImplicitOperator_ConvertsStringToEmailValido()
    {
        var valor = "email@example.com";

        EmailValido emailValido = valor;

        Assert.Equal(valor, emailValido.Valor);
    }

    [Fact]
    public void ImplicitOperator_ConvertsEmailValidoToString()
    {
        var valor = "email@example.com";
        var emailValido = new EmailValido(valor);

        string result = emailValido;

        Assert.Equal(valor, result);
    }

    [Fact]
    public void Equals_ComparaDoisEmailValido()
    {
        var emailValido1 = new EmailValido("email@example.com");
        var emailValido2 = new EmailValido("email@example.com");

        Assert.True(emailValido1.Equals(emailValido2));
    }

    [Fact]
    public void Equals_ComparaEmailValidoComOutroTipo()
    {
        var emailValido = new EmailValido("email@example.com");
        var outroTipo = "outro tipo";

        Assert.False(outroTipo.Equals(emailValido.Valor));
    }
}