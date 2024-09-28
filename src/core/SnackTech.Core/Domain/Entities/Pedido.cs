using System;
using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class Pedido(Guid id, DataPedidoValida dataCriacao, StatusPedidoValido status, Cliente cliente)
{
    internal Guid Id { get; private set; }
    internal DataPedidoValida DataCriacao { get; private set; }
    internal Cliente Cliente { get; private set; }
    internal StatusPedidoValido Status { get; private set; }
    internal List<PedidoItem> Itens { get; private set; } = new();

    public Pedido(Guid id, DataPedidoValida dataCriacao, StatusPedidoValido status, Cliente cliente, List<PedidoItem> itens)
        : this(id, dataCriacao, status, cliente)
    {
        Itens = itens;
    }

    public Pedido(PedidoDto pedidoDto) : this(pedidoDto.Id, pedidoDto.DataCriacao, pedidoDto.Status, (Cliente)pedidoDto.Cliente)
    {
        Itens = pedidoDto.Itens.Select(item => (PedidoItem)item).ToList();
    }

    public static implicit operator Pedido(PedidoDto pedidoDto)
    {
        return new Pedido(pedidoDto);
    }

    public static implicit operator PedidoDto(Pedido pedido)
    {
        return new PedidoDto
        {
            Id = pedido.Id,
            DataCriacao = pedido.DataCriacao.Valor,
            Status = pedido.Status.Valor,
            Cliente = (ClienteDto)pedido.Cliente,
            Itens = pedido.Itens.Select(item => (PedidoItemDto)item).ToList()
        };
    }
}
