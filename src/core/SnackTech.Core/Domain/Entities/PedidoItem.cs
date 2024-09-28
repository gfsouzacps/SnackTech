using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class PedidoItem(GuidValido id, Produto produto, InteiroPositivo quantidade, string observacao)
{
    internal GuidValido Id { get; private set; } = id;
    internal InteiroPositivo Quantidade { get; private set; } = quantidade;
    internal string Observacao { get; private set; } = observacao;
    internal Produto Produto { get; private set; } = produto;

    public PedidoItem(PedidoItemRetornoDto pedidoItemDto) : this(pedidoItemDto.Id, pedidoItemDto.Produto, pedidoItemDto.Quantidade, pedidoItemDto.Observacao) { }

    public static implicit operator PedidoItem(PedidoItemRetornoDto pedidoItemDto)
    {
        return new(pedidoItemDto);
    }

    public static implicit operator PedidoItemRetornoDto(PedidoItem pedidoItem)
    {
        return new PedidoItemRetornoDto
        {
            Id = pedidoItem.Id,
            Quantidade = pedidoItem.Quantidade.Valor,
            Observacao = pedidoItem.Observacao,
            Valor = pedidoItem.Valor(),
            Produto = (ProdutoDto)pedidoItem.Produto
        };
    }

    internal DecimalPositivo Valor()
    {
        return Quantidade.Valor * Produto.Valor.Valor;
    }
}
