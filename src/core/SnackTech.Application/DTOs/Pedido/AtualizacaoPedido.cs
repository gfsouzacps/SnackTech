namespace SnackTech.Application.DTOs.Pedido
{
    public class AtualizacaoPedido
    {
        public string Identificacao {get; set;} = string.Empty;
        public required IEnumerable<AtualizacaoPedidoItem> PedidoItens {get; set;}
    }

    public class AtualizacaoPedidoItem{
        public int Sequencial {get; set;}
        public string IdentificacaoProduto {get; set;} = string.Empty;
        public int Quantidade {get; set;}
        public string Observacao {get; set;} = string.Empty;

    }
}