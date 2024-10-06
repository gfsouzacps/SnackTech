using SnackTech.Common.Dto.Api;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class Pedido
{
    internal GuidValido Id { get; private set; }
    internal DataPedidoValida DataCriacao { get; private set; }
    internal Cliente Cliente { get; private set; }
    internal StatusPedidoValido Status { get; private set; }
    internal List<PedidoItem> Itens { get; set; }

    public Pedido(GuidValido id, DataPedidoValida dataCriacao, StatusPedidoValido status, Cliente cliente)
    {
        Id = id;
        DataCriacao = dataCriacao;
        Status = status;
        
        if(cliente == null) throw new ArgumentException("O cliente deve ser informado.", nameof(cliente));
        
        Cliente = cliente;
        Itens = new List<PedidoItem>();
    }

    public Pedido(GuidValido id, DataPedidoValida dataCriacao, StatusPedidoValido status, Cliente cliente, List<PedidoItem> itens)
        : this(id, dataCriacao, status, cliente)
    {
        if(itens != null) Itens = itens;
    }

    internal void FecharPedidoParaPagamento()
    {
        if (Itens.Count == 0)
            throw new Exception("O pedido deve ter pelo menos um item para ser finalizado para pagamento.");

        Status = StatusPedidoValido.AguardandoPagamento;
    }
}
