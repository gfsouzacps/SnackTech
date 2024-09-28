using System;
using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class PedidoItem(Guid id, Produto produto, InteiroPositivo quantidade, DecimalPositivo valor, string observacao)
{
    internal Guid Id { get; private set; } = id;
    internal InteiroPositivo Quantidade { get; private set; } = quantidade;
    internal string Observacao { get; private set; } = observacao;
    internal DecimalPositivo Valor { get; private set; } = valor;
    internal Produto Produto { get; private set; } = produto;

    public PedidoItem(PedidoItemDto pedidoItemDto) : this(pedidoItemDto.Id, pedidoItemDto.Produto, pedidoItemDto.Quantidade, pedidoItemDto.Valor, pedidoItemDto.Observacao) { }

    public static implicit operator PedidoItem(PedidoItemDto pedidoItemDto)
    {
        return new(pedidoItemDto);
    }

    public static implicit operator PedidoItemDto(PedidoItem pedidoItem)
    {
        return new PedidoItemDto
        {
            Id = pedidoItem.Id,
            Quantidade = pedidoItem.Quantidade.Valor,
            Observacao = pedidoItem.Observacao,
            Valor = pedidoItem.Valor.Valor,
            Produto = (ProdutoDto)pedidoItem.Produto
        };
    }
}
