using SnackTech.Domain.Enums;

namespace SnackTech.Driver.DataBase.Entities
{
    public class Pedido
    {

        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public Cliente Cliente { get; set; } = new();
        public StatusPedido Status { get; set; }        
        public List<PedidoItem> Itens { get; set; } = new();
    }
}
