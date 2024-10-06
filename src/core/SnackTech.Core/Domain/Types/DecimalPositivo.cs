namespace SnackTech.Core.Domain.Types
{
    internal struct DecimalPositivo : IEquatable<DecimalPositivo>
    {
        private decimal valor;

        internal decimal Valor{
            readonly get {return valor;}
            set{
                ValidarValor(value);
                valor = value;
            }
        }

        internal DecimalPositivo(decimal valor){
            Valor = valor;
        }

        public static implicit operator DecimalPositivo(decimal valor){
            return new DecimalPositivo(valor);
        }

        public static implicit operator decimal(DecimalPositivo valor){
            return valor.Valor;
        }

        public override readonly string ToString()
        {
            return Valor.ToString();
        }

        private static void ValidarValor(decimal value){
            if(value <= 0){
                throw new ArgumentException("O valor precisa ser maior que zero");
            }
        }

        public bool Equals(DecimalPositivo other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Valor == other.Valor;
        }
    }
}