using SnackTech.Common.Dto.Api;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class Pedido(GuidValido id, DataPedidoValida dataCriacao, StatusPedidoValido status, Cliente cliente)
{
    internal GuidValido Id { get; private set; } = id;
    internal DataPedidoValida DataCriacao { get; private set; } = dataCriacao;
    internal Cliente Cliente { get; private set; } = cliente;
    internal StatusPedidoValido Status { get; private set; } = status;
    internal List<PedidoItem> Itens { get; set; } = new();

    public Pedido(GuidValido id, DataPedidoValida dataCriacao, StatusPedidoValido status, Cliente cliente, List<PedidoItem> itens)
        : this(id, dataCriacao, status, cliente)
    {
        Itens = itens;
    }

    internal void FecharPedidoParaPagamento()
    {
        if (Itens.Count == 0)
            throw new Exception("O pedido deve ter pelo menos um item para ser finalizado para pagamento.");

        Status = StatusPedidoValido.AguardandoPagamento;
    }
}
