namespace SnackTech.Core.Domain.Types
{
    public class StringNaoVaziaOuComEspacos
    {
        private string valor = default!;

        public string Valor {
            get {return valor;}
            set{
                ValidarValorString(value);
                valor = value;
            }
        }

        public StringNaoVaziaOuComEspacos(string value){
            Valor = value;
        }

        public static implicit operator StringNaoVaziaOuComEspacos(string value){
            return new StringNaoVaziaOuComEspacos(value);
        }

        public override string ToString()
            => Valor;

        private static void ValidarValorString(string value){
            if(string.IsNullOrWhiteSpace(value)){
                    throw new ArgumentException("O valor atribuído não pode ser nulo, vazio ou somente com espaços");
            }
        }
        
    }
}