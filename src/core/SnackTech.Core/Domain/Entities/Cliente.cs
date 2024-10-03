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
}
