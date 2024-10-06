namespace SnackTech.Core.Domain.Types
{
    public struct ActionPagamentoValido
    {
        private string valor;

        private readonly IEnumerable<string> ValoresValidos = new string[]{"update","create"};

        internal string Valor{
            readonly get {return valor;}
            set{
                ValidarValor(value);
                valor = value;
            }
        }

        private readonly void ValidarValor(string value)
        {
            if (!ValoresValidos.Any((chaveValor) => chaveValor == value))
            {
                throw new ArgumentException($"Valor {value} não é uma Categoria de Produto Válida");
            }
        }

        internal ActionPagamentoValido(string valor){
            this.valor = valor;
        }

        public static implicit operator ActionPagamentoValido(string value){
            return new ActionPagamentoValido(value);
        }

        public static implicit operator string(ActionPagamentoValido valor){
            return valor.Valor;
        }

        public readonly override string ToString()
            => Valor.ToString();
    }
}