namespace SnackTech.Core.Domain.Types
{
    internal struct CategoriaProdutoValido : IEquatable<CategoriaProdutoValido>
    {
        private int valor;
        private readonly Dictionary<int, string> ValoresValidos = new()
        {
            {1,"Lanche"},
            {2,"Acompanhamento"},
            {3,"Bebida"},
            {4,"Sobremesa"},
        };

        internal CategoriaProdutoValido(int valor)
        {
            Valor = valor;
        }

        internal int Valor
        {
            readonly get { return valor; }
            set
            {
                ValidarValor(value);
                valor = value;
            }
        }

        public static implicit operator CategoriaProdutoValido(int value)
        {
            return new CategoriaProdutoValido(value);
        }

        public static implicit operator int(CategoriaProdutoValido valor)
        {
            return valor.Valor;
        }

        public override readonly string ToString()
            => Valor.ToString();

        private readonly void ValidarValor(int value)
        {
            if (!ValoresValidos.Any((chaveValor) => chaveValor.Key == value))
            {
                throw new ArgumentException($"Valor {value} não é uma Categoria de Produto Válida");
            }
        }

        public bool Equals(CategoriaProdutoValido other)
        {
            if (other == null) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.Valor == other.Valor;
        }
    }
}
