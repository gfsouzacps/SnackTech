namespace SnackTech.Driver.MercadoPago.Payloads
{
    public class CriarPedido
    {
        public string external_reference {get; set;} = default!;
        public string title {get; set;} = default!;
        public string description {get; set;} = default!;
        public decimal total_amount {get; set;}
        public required IEnumerable<CriarPedidoItem> items {get; set;}
    }


    public class CriarPedidoItem{
        public string sku_number {get; set;} = default!;
        public string category {get; set;} = default!;
        public string title {get; set;} = default!;
        public string description {get; set;} = default!;
        public decimal unit_price {get; set;}
        public int quantity {get; set;}
        public string unit_measure {get; set;} = default!;
        public decimal total_amount {get; set;}
    }
}