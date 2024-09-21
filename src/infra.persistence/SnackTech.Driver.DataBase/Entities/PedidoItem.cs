
namespace SnackTech.Driver.DataBase.Entities
{
    public class PedidoItem
    {
        public Guid Id {get; set;}        
        public int Sequencial {get; set;}   
        public int Quantidade {get; set;}   
        public string Observacao {get; set;} = string.Empty;
        public decimal Valor {get; set;}  
        public Produto Produto {get; set;} = new();
        public Pedido Pedido {get; set;} = new();

    }
}