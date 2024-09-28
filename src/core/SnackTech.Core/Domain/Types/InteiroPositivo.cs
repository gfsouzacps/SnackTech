namespace SnackTech.Core.Domain.Types
{
    internal struct InteiroPositivo
    {
        private int valor;

        internal int Valor
        {
            readonly get { return valor; }
            set
            {
                ValidarValor(value);
                valor = value;
            }
        }

        internal InteiroPositivo(int valor)
        {
            Valor = valor;
        }

        public static implicit operator InteiroPositivo(int valor)
        {
            return new InteiroPositivo(valor);
        }

        public static implicit operator int(InteiroPositivo valor)
        {
            return valor.Valor;
        }

        public override readonly string ToString()
        {
            return Valor.ToString();
        }

        private static void ValidarValor(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("O valor precisa ser maior que zero");
            }
        }
    }
}
