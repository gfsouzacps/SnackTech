using System;
using System.Globalization;
using SnackTech.Common.Dto;
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

    public Cliente(ClienteDto clienteDto) : this(clienteDto.Id, clienteDto.Nome, clienteDto.Email, clienteDto.Cpf) { }

    public static implicit operator Cliente(ClienteDto clienteDto)
    {
        return new Cliente(clienteDto);
    }

    public static implicit operator ClienteDto(Cliente cliente)
    {
        return new ClienteDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome.Valor,
            Email = cliente.Email.Valor,
            Cpf = cliente.Cpf.Valor
        };
    }
}
