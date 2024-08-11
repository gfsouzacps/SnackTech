
namespace SnackTech.Adapter.DataBase.Entities
{
    public class PedidoItem
    {
        public Guid Id {get; set;}        
        public int Sequencial {get; set;}   
        public int Quantidade {get; set;}   
        public string Observacao {get; set;} 
        public decimal Valor {get; set;}  
        public Produto Produto {get; set;}
        public Pedido Pedido {get; set;}   

    }
}