namespace SnackTech.Core.Domain.Types
{
    internal class StringNaoVazia
    {
        private string valor = default!;

        internal string Valor {
            get {return valor;}
            set{
                ValidarValorString(value);
                valor = value;
            }
        }

        internal StringNaoVazia(string value){
            Valor = value;
        }

        public static implicit operator StringNaoVazia(string value){
            return new StringNaoVazia(value);
        }

        public override string ToString()
            => Valor;

        private static void ValidarValorString(string value){
            if(string.IsNullOrEmpty(value)){
                    throw new ArgumentException("O valor atribuído não pode ser nulo ou vazio");
            }
        }
    }
}