using System;
using FluentAssertions;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Presenters;

namespace SnackTech.Core.Tests.Presenters;

public class ClientePresenterTest
{
    private Cliente _clienteExemplo;

    public ClientePresenterTest()
    {
        _clienteExemplo = new Cliente(Guid.NewGuid(), "João", "joao@example.com", "00000000191");
    }
    [Fact]
    public void ConverterParaDto_DeveRetornarClienteDtoComDadosCorretos()
    {
       var clienteDto = ClientePresenter.ConverterParaDto(_clienteExemplo);
    
        // Assert
        clienteDto.Should().NotBeNull();
        clienteDto.IdentificacaoCliente.Should().Be(_clienteExemplo.Id.Valor);
        clienteDto.Nome.Should().Be(_clienteExemplo.Nome.Valor);
        clienteDto.Email.Should().Be(_clienteExemplo.Email.Valor);
        clienteDto.Cpf.Should().Be(_clienteExemplo.Cpf.Valor);
    }
    
    [Fact]
    public void ApresentarResultadoCliente_DeveRetornarResultadoOperacaoComClienteDto()
    {    
        // Act
        var resultado = ClientePresenter.ApresentarResultadoCliente(_clienteExemplo);
    
        // Assert
        resultado.Should().NotBeNull();
        resultado.Dados.Should().NotBeNull();
        resultado.Dados.IdentificacaoCliente.Should().Be(_clienteExemplo.Id.Valor);
        resultado.Dados.Nome.Should().Be(_clienteExemplo.Nome.Valor);
        resultado.Dados.Email.Should().Be(_clienteExemplo.Email.Valor);
        resultado.Dados.Cpf.Should().Be(_clienteExemplo.Cpf.Valor);
    }
}
