namespace SnackTech.Core.Domain.Types
{
    public struct DecimalPositivo
    {
        private decimal valor;

        public decimal Valor{
            readonly get {return valor;}
            set{
                ValidarValor(value);
                valor = value;
            }
        }

        public DecimalPositivo(decimal valor){
            Valor = valor;
        }

        public static implicit operator DecimalPositivo(decimal valor){
            return new DecimalPositivo(valor);
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
    }
}