namespace SnackTech.Application.DTOs.Pedido
{
    public class RetornoPedido
    {
        public string Identificacao {get; set;} = string.Empty;
        public string NomeCliente {get; set;} = string.Empty;
        public string CPFCliente {get; set;} = string.Empty;        
        public int Status {get; set;}
        public decimal Valor {get; set;}
        public required IEnumerable<RetornoPedidoItem> Itens {get; set;}
    }
}