using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class Cliente(GuidValido id, StringNaoVaziaOuComEspacos nome, EmailValido email, CpfValido cpf)
{
    //Esse número de CPF é válido segundo o algoritmo de validação
    internal const string CPF_CLIENTE_PADRAO = "00000000191";
    internal GuidValido Id { get; private set; } = id;
    internal StringNaoVaziaOuComEspacos Nome { get; private set; } = nome;
    internal EmailValido Email { get; private set; } = email;
    internal CpfValido Cpf { get; private set; } = cpf;

    public Cliente(Common.Dto.Api.ClienteDto clienteDto) : this(clienteDto.Id, clienteDto.Nome, clienteDto.Email, clienteDto.Cpf) { }
    public Cliente(Common.Dto.DataSource.ClienteDto clienteDto) : this(clienteDto.Id, clienteDto.Nome, clienteDto.Email, clienteDto.Cpf) { }

    public static implicit operator Cliente(Common.Dto.Api.ClienteDto clienteDto)
    {
        return new Cliente(clienteDto);
    }

    public static implicit operator Cliente(Common.Dto.DataSource.ClienteDto clienteDto)
    {
        return new Cliente(clienteDto);
    }

    public static implicit operator Common.Dto.Api.ClienteDto(Cliente cliente)
    {
        return new Common.Dto.Api.ClienteDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome.Valor,
            Email = cliente.Email.Valor,
            Cpf = cliente.Cpf.Valor
        };
    }

    public static implicit operator Common.Dto.DataSource.ClienteDto(Cliente cliente)
    {
        return new Common.Dto.DataSource.ClienteDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome.Valor,
            Email = cliente.Email.Valor,
            Cpf = cliente.Cpf.Valor
        };
    }
}
