using SnackTech.Domain.Enums;
using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Models
{
    public class Produto
    {
        public Guid Id {get; private set;}
        public CategoriaProduto Categoria {get; private set;}
        public string Nome {get; private set;}
        public string Descricao {get; private set;}
        public decimal Valor {get; private set;}

        public Produto(CategoriaProduto categoriaProduto, string nome, string descricao, decimal valor)
            :this(Guid.NewGuid(),categoriaProduto,nome,descricao,valor)
        {}

        public Produto(Guid id, CategoriaProduto categoriaProduto, string nome, string descricao, decimal valor)
        {
            CustomGuards.AgainstStringNullOrWhiteSpace(nome, nameof(nome));
            CustomGuards.AgainstStringNullOrEmpty(descricao, nameof(descricao));
            CustomGuards.AgainstNegativeOrZeroValue(valor, nameof(valor));

            Id = id;
            Categoria = categoriaProduto;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
        }
    }
}