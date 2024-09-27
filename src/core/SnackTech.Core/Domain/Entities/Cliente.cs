using System;
using System.Globalization;
using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class Cliente(Guid id, StringNaoVaziaOuComEspacos nome, EmailValido email, CpfValido cpf)
{
    //Esse número de CPF é válido segundo o algoritmo de validação
    public const string CPF_CLIENTE_PADRAO = "00000000191";
    public Guid Id { get; protected set; } = id;
    public StringNaoVaziaOuComEspacos Nome { get; protected set; } = nome;
    public EmailValido Email { get; private set; } = email;
    public CpfValido Cpf { get; private set; } = cpf;

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
