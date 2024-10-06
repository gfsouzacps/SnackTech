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
            var listaItens = pedidoDto.Itens.Select(i => new CriarPedidoItem(i));
            
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

        public CriarPedidoItem(PedidoItemDto pedidoItemDto){
            var produto = pedidoItemDto.Produto;
            sku_number = produto.Id.ToString();
            category = produto.Categoria.ToString();
            title = produto.Nome;
            description = produto.Descricao;
            unit_price = produto.Valor;
            quantity = pedidoItemDto.Quantidade;
            unit_measure = "unit";
            total_amount = produto.Valor * pedidoItemDto.Quantidade;
        }
    }
}