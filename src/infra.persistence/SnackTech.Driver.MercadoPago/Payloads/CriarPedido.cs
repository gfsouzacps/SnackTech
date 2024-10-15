using SnackTech.Common.Dto.DataSource;

namespace SnackTech.Driver.MercadoPago.Payloads
{
    public class CriarPedido
    {
        public string external_reference {get; set;}
        public string title {get; set;}
        public string description {get; set;}
        public decimal total_amount {get; set;}
        public IEnumerable<CriarPedidoItem> items {get; set;}

        public CriarPedido(PedidoDto pedidoDto){
            var listaItens = new CriarPedidoItem[]{new(pedidoDto.Itens)};
            
            items = listaItens;
            total_amount = listaItens.Sum(l => l.total_amount);

            external_reference = pedidoDto.Id.ToString();
            title = $"Pedido-{pedidoDto.Id}";
            description = $"SnackTech-Pedido-{pedidoDto.Id}";
        }
    }


    public class CriarPedidoItem{
        public string sku_number {get; set;}
        public string category {get; set;}
        public string title {get; set;}
        public string description {get; set;}
        public decimal unit_price {get; set;}
        public int quantity {get; set;}
        public string unit_measure {get; set;}
        public decimal total_amount {get; set;}

        public CriarPedidoItem(IEnumerable<PedidoItemDto> itensDoPedido){
            var valor = itensDoPedido.Sum(i => i.Produto.Valor * i.Quantidade);
            sku_number = "produto-001";
            category = "Combo";
            title = "Combo SnackTech";
            description = "Conjunto de produtos da lanchonete SnackTeck";
            unit_price = valor;
            quantity = 1;
            unit_measure = "unit";
            total_amount = valor;
        }
    }
}