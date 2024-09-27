using System;

namespace SnackTech.Core.Domain.Entities;

internal class PedidoItem(Guid id, Guid pedidoId, Guid produtoId, int quantidade, decimal valor, string observacao = string.Empty)
{
    internal Guid Id { get; private set; }
    internal int Quantidade { get; private set; }
    internal string Observacao { get; private set; }
    internal decimal Valor { get; private set; }
    internal Produto Produto { get; private set; }
    internal Pedido Pedido { get; private set; }
}
