
namespace SnackTech.Driver.DataBase.Entities
{
    public class PedidoItem
    {
        public Guid Id { get; set; }
        public int Quantidade { get; set; }
        public string Observacao { get; set; }
        public decimal Valor { get; set; }
        public Produto Produto { get; set; }
    }
}