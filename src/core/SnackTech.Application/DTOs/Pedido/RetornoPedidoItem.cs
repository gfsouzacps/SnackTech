namespace SnackTech.Application.DTOs.Pedido
{
    public class RetornoPedidoItem
    {
        public int Sequencial {get; set;}        
        public int CategoriaProduto {get; set;}
        public string NomeProduto {get; set;} = string.Empty;
        public decimal ValorProduto {get; set;}
        public int Quantidade {get; set;}
        public string Observacao {get; set;} = string.Empty;
        public decimal ValorItem {get;set;}
    }
}