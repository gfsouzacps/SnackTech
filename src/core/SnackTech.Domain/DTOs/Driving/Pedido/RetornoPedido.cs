using DomainModels = SnackTech.Domain.Models;

namespace SnackTech.Domain.DTOs.Driving.Pedido
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
                NomeCliente = pedido.Cliente.Nome,
                CPFCliente = pedido.Cliente.Cpf,
                Status = (int)pedido.Status,
                Valor = pedido.Valor,
                Itens = pedido.Itens.Select(item => RetornoPedidoItem.APartirDeItem(item)).ToArray(),
            };

    }
}
