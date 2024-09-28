using System;
using SnackTech.Core.Domain.Util;

namespace SnackTech.Core.Domain.Types;

internal struct CpfValido
{
    private string cpf;

    internal string Valor
    {
        readonly get { return cpf; }
        private set
        {
            var cpfSemFormatacao = value.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            ValidarValor(cpfSemFormatacao);
            cpf = cpfSemFormatacao;
        }
    }

    internal CpfValido(string cpf)
    {
        Valor = cpf;
    }

    public static implicit operator CpfValido(string cpf)
    {
        return new CpfValido(cpf);
    }

    public static implicit operator string(CpfValido cpf)
    {
        return cpf.Valor;
    }

    public override readonly string ToString()
    {
        return cpf.ToString();
    }

    private static void ValidarValor(string cpfValue)
    {
        CpfValidator.AgainstInvalidCpf(cpfValue);
    }
}
