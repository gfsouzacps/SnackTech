
using SnackTech.Domain.Enums;

namespace SnackTech.Driver.DataBase.Entities
{
    public class Produto
    {
        public Guid Id {get; set;}
        public CategoriaProduto Categoria {get; set;}
        public string Nome {get; set;} = string.Empty;
        public string Descricao {get; set;} = string.Empty;
        public decimal Valor {get; set;}
    }
}