using System;

namespace SnackTech.Core.Domain.Entities;

internal class Pedido(Guid id, DateTime dataCriacao, StatusPedido status, Cliente cliente)
{
    internal Guid Id { get; private set; }
    internal DateTime DataCriacao { get; private set; }
    internal Cliente Cliente { get; private set; }
    internal StatusPedido Status { get; private set; }
    internal List<PedidoItem> Itens { get; private set; } = new();

    public Pedido(Guid id, DateTime dataCriacao, StatusPedido status, Cliente cliente, List<PedidoItem> itens)
        : this(id, dataCriacao, status, cliente)
    {
        Itens = itens;
    }
}
