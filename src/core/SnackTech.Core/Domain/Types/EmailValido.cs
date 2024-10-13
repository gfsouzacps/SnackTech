using System.Net.Mail;

namespace SnackTech.Core.Domain.Types;

internal struct EmailValido : IEquatable<EmailValido>
{
    private string enderecoEmail;

    internal string Valor
    {
        readonly get { return enderecoEmail; }
        set
        {
            ValidarValor(value);
            enderecoEmail = value;
        }
    }

    internal EmailValido(string email)
    {
        Valor = email;
    }

    public static implicit operator EmailValido(string email)
    {
        return new EmailValido(email);
    }

    public static implicit operator string(EmailValido email)
    {
        return email.Valor;
    }

    public override readonly string ToString()
    {
        return enderecoEmail.ToString();
    }

    private static void ValidarValor(string emailValue)
    {
        if (string.IsNullOrWhiteSpace(emailValue))
        {
            throw new ArgumentException("O valor atribuído não pode ser nulo, vazio ou somente com espaços");
        }

        if (!IsValidEmail(emailValue))
        {
            throw new ArgumentException($"O valor {emailValue} não é um e-mail válido.");
        }
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new MailAddress(email);
            return mailAddress != null;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public bool Equals(EmailValido other)
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;

        return this.Valor == other.Valor;
    }
}
