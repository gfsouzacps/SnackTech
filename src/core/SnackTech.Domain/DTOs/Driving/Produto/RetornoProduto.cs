using DomainModels = SnackTech.Domain.Entities;

namespace SnackTech.Domain.DTOs.Driving.Produto
{
    public class RetornoProduto
    {
        public string Identificacao {get; set;} = string.Empty;
        public int Categoria {get; set;}
        public string Nome {get; set;} = string.Empty;
        public string Descricao {get; set;} = string.Empty;
        public decimal Valor {get; set;}

        public static RetornoProduto APartirDeProduto(DomainModels.Produto produto)
        => new(){
            Identificacao = produto.Id.ToString(),
            Categoria = (int)produto.Categoria,
            Descricao = produto.Descricao,
            Nome = produto.Nome,
            Valor = produto.Valor
        };
    }
}