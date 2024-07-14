
namespace SnackTech.Application.DTOs.Produto
{
    public class EdicaoProduto
    {
        public string Identificacao {get; set;} = string.Empty;
        public int Categoria {get; set;}
        public string Nome {get; set;} = string.Empty;
        public string Descricao {get; set;} = string.Empty;
        public decimal Valor {get; set;}        
    }
}