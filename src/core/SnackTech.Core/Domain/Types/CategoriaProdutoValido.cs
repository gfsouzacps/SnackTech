namespace SnackTech.Core.Domain.Types
{
    public struct CategoriaProdutoValido
    {
        private int valor;
        private readonly Dictionary<int,string> ValoresValidos = new()
        {
            {1,"Lanche"},
            {2,"Acompanhamento"},
            {3,"Bebida"},
            {4,"Sobremesa"},
        };

        public CategoriaProdutoValido(int valor)
        {
            Valor = valor;
        }

        public int Valor{
            readonly get {return valor;}
            set{
                ValidarValor(value);
                valor = value;
            }
        }

        public static implicit operator CategoriaProdutoValido(int value){
            return new CategoriaProdutoValido(value);
        }

        public override readonly string ToString()
            => Valor.ToString();

        private readonly void ValidarValor(int value){
            if (!ValoresValidos.Any((chaveValor) => chaveValor.Key == value)){
                throw new ArgumentException($"Valor {value} não é uma Categoria de Produto Válida");
            }
        }
    }
}