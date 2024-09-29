using SnackTech.Common.Dto.Api;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class Pedido(GuidValido id, DataPedidoValida dataCriacao, StatusPedidoValido status, Cliente cliente)
{
    internal GuidValido Id { get; private set; }
    internal DataPedidoValida DataCriacao { get; private set; }
    internal Cliente Cliente { get; private set; }
    internal StatusPedidoValido Status { get; private set; }
    internal List<PedidoItem> Itens { get; set; } = new();

    public Pedido(GuidValido id, DataPedidoValida dataCriacao, StatusPedidoValido status, Cliente cliente, List<PedidoItem> itens)
        : this(id, dataCriacao, status, cliente)
    {
        Itens = itens;
    }

    public Pedido(PedidoRetornoDto pedidoDto) : this(pedidoDto.Id, pedidoDto.DataCriacao, pedidoDto.Status, (Cliente)pedidoDto.Cliente)
    {
        Itens = pedidoDto.Itens.Select(item => (PedidoItem)item).ToList();
    }
    public Pedido(PedidoDto pedidoDto) : this(pedidoDto.Id, pedidoDto.DataCriacao, pedidoDto.Status, (Cliente)pedidoDto.Cliente)
    {
        Itens = pedidoDto.Itens.Select(item => (PedidoItem)item).ToList();
    }

    public static implicit operator Pedido(PedidoRetornoDto pedidoDto)
    {
        return new Pedido(pedidoDto);
    }

    public static implicit operator Pedido(PedidoDto pedidoDto)
    {
        return new Pedido(pedidoDto);
    }

    public static implicit operator PedidoRetornoDto(Pedido pedido)
    {
        return new PedidoRetornoDto
        {
            Id = pedido.Id,
            DataCriacao = pedido.DataCriacao.Valor,
            Status = pedido.Status.Valor,
            Cliente = (Common.Dto.Api.ClienteDto)pedido.Cliente,
            Itens = pedido.Itens.Select(item => (PedidoItemRetornoDto)item).ToList()
        };
    }

    public static implicit operator PedidoDto(Pedido pedido)
    {
        return new PedidoDto
        {
            Id = pedido.Id,
            DataCriacao = pedido.DataCriacao.Valor,
            Status = pedido.Status.Valor,
            Cliente = (Common.Dto.DataSource.ClienteDto)pedido.Cliente,
            Itens = pedido.Itens.Select(item => (PedidoItemDto)item).ToList()
        };
    }

    internal void FecharPedidoParaPagamento()
    {
        if (Itens.Count == 0)
            throw new Exception("O pedido deve ter pelo menos um item para ser finalizado para pagamento.");

        Status = StatusPedidoValido.AguardandoPagamento;
    }
}
