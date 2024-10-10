namespace SnackTech.Core.Domain.Types;

internal struct DataPedidoValida : IEquatable<DataPedidoValida>
{
    static readonly DateTime DATA_MINIMA = new DateTime(2024, 1, 1, 0, 0, 0, 0);

    private DateTime valor;

    internal DateTime Valor
    {
        readonly get { return valor; }
        set
        {
            ValidarValor(value);
            valor = value;
        }
    }

    internal DataPedidoValida(DateTime valor)
    {
        Valor = valor;
    }

    public static implicit operator DataPedidoValida(DateTime value)
    {
        return new DataPedidoValida(value);
    }

    public static implicit operator DateTime(DataPedidoValida valor)
    {
        return valor.Valor;
    }

    public override readonly string ToString()
           => Valor.ToString();

    private static void ValidarValor(DateTime value)
    {
        if (value < DATA_MINIMA)
        {
            throw new ArgumentException("Não é permitido criar pedidos com data anterior a 01/01/2024.");
        }

        if (value > DateTime.Now)
        {
            throw new ArgumentException("Não é permitido criar pedidos com data/horário futuro.");
        }
    }

    public bool Equals(DataPedidoValida other)
    {
        if (ReferenceEquals(this, other)) return true;

        return this.Valor == other.Valor;
    }
}
