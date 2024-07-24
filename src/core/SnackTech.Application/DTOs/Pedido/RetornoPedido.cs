using SnackTech.Application.DTOs.Produto;
using DomainModels = SnackTech.Domain.Models;

namespace SnackTech.Application.DTOs.Pedido
{
    public class RetornoPedido
    {
        public string Identificacao { get; set; } = string.Empty;
        public string NomeCliente { get; set; } = string.Empty;
        public string CPFCliente { get; set; } = string.Empty;
        public int Status { get; set; }
        public decimal Valor { get; set; }
        public required IEnumerable<RetornoPedidoItem> Itens { get; set; }

        public static RetornoPedido APartirDePedido(DomainModels.Pedido pedido)
            => new()
            {
                Identificacao = pedido.Id.ToString(),
                NomeCliente = pedido.Cliente.RecuperarNome(),
                CPFCliente = pedido.Cliente.CPF,
                Status = (int)pedido.Status,
                Valor = pedido.Valor,
                Itens = pedido.Itens.Select(item => RetornoPedidoItem.APartirDeItem(item)).ToArray(),
            };

    }
}
