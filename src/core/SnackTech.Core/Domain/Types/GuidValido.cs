namespace SnackTech.Core.Domain.Types;

internal struct GuidValido : IEquatable<GuidValido>
{
    internal Guid Valor { readonly get; private set; }

    internal GuidValido(string guid)
    {
        Valor = ValidarValor(guid);
    }

    internal GuidValido(Guid guid)
    {
        Valor = guid;
    }

    public static implicit operator GuidValido(string guid)
    {
        return new GuidValido(guid);
    }

    public static implicit operator Guid(GuidValido guid)
    {
        return guid.Valor;
    }

    public static implicit operator GuidValido(Guid guid)
    {
        return new GuidValido(guid);
    }

    public static implicit operator string(GuidValido guid)
    {
        return guid.Valor.ToString();
    }

    public override readonly string ToString()
    {
        return Valor.ToString();
    }

    private static Guid ValidarValor(string guidValue)
    {
        if (Guid.TryParse(guidValue, out Guid guid))
        {
            return guid;
        }
        else
        {
            throw new ArgumentException($"A Identifica��o informada {guidValue} n�o � um Guid v�lido.");
        }
    }

    public bool Equals(GuidValido other)
    {
        if (other == null) return false;
        if (ReferenceEquals(this, other)) return true;

        return this.Valor == other.Valor;
    }
}
