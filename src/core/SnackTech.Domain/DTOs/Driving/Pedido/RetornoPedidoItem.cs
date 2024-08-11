using SnackTech.Domain.Models;

namespace SnackTech.Domain.DTOs.Driving.Pedido
{
    public class RetornoPedidoItem
    {
        public int Sequencial { get; set; }
        public int CategoriaProduto { get; set; }
        public string NomeProduto { get; set; } = string.Empty;
        public decimal ValorProduto { get; set; }
        public int Quantidade { get; set; }
        public string Observacao { get; set; } = string.Empty;
        public decimal ValorItem { get; set; }

        internal static RetornoPedidoItem APartirDeItem(PedidoItem item)
        {
            return new RetornoPedidoItem
            {
                Sequencial = item.Sequencial,
                CategoriaProduto = (int)item.Produto.Categoria,
                NomeProduto = item.Produto.Nome,
                ValorProduto = item.Produto.Valor,
                Quantidade = item.Quantidade,
                Observacao = item.Observacao,
                ValorItem = item.Valor
            };
        }
    }
}
