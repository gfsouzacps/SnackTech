using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class Pedido(GuidValido id, DataPedidoValida dataCriacao, StatusPedidoValido status, Cliente cliente)
{
    internal GuidValido Id { get; private set; } = id;
    internal DataPedidoValida DataCriacao { get; private set; } = dataCriacao;
    internal Cliente Cliente { get; private set; } = cliente;
    internal StatusPedidoValido Status { get; private set; } = status;
    internal List<PedidoItem> Itens { get; set; } = [];
    internal DecimalPositivo ValorTotal {get; private set;}

    public Pedido(GuidValido id, DataPedidoValida dataCriacao, StatusPedidoValido status, Cliente cliente, List<PedidoItem> itens)
        : this(id, dataCriacao, status, cliente)
    {
        Itens = itens;
        ValorTotal = Itens.Sum(i => i.Valor());
    }

    internal void FecharPedidoParaPagamento()
    {
        if (Itens.Count == 0)
            throw new ArgumentException("O pedido deve ter pelo menos um item para ser finalizado para pagamento.");

        if (ValorTotal <= 0)
            throw new ArgumentException("O cálculo do Valor total do pedido está resultando em um valor menor ou igual a zero.");

        if(Status != StatusPedidoValido.Iniciado)
            throw new ArgumentException("Pedido está com status diferente de Iniciado, não será possível movê-lo para aguardar pagamento");

        Status = StatusPedidoValido.AguardandoPagamento;
        
    }
}
