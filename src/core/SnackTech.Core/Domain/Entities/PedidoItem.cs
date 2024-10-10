using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class PedidoItem(GuidValido id, Produto produto, InteiroPositivo quantidade, string observacao)
{
    internal GuidValido Id { get; private set; } = id;
    internal InteiroPositivo Quantidade { get; private set; } = quantidade;
    internal string Observacao { get; private set; } = observacao;
    internal Produto Produto { get; private set; } = produto;

    internal DecimalPositivo Valor()
    {
        return Quantidade.Valor * Produto.Valor.Valor;
    }

    internal void Atualizar(InteiroPositivo quantidade, Produto produto, string observacao)
    {
        Quantidade = quantidade;
        Produto = produto;
        Observacao = observacao;
    }
}
