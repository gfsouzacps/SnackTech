using SnackTech.Common.Dto.Api;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class PedidoItem
{
    internal GuidValido Id { get; private set; } 
    internal InteiroPositivo Quantidade { get; private set; } 
    internal string Observacao { get; private set; } 
    internal Produto Produto { get; private set; } 

    public PedidoItem(GuidValido id, Produto produto, InteiroPositivo quantidade, string observacao)
    {
        if(produto is null) throw new ArgumentException("O produto não pode ser nulo.", nameof(produto));   

        Id = id;
        Quantidade = quantidade;
        Observacao = observacao;
        Produto = produto;
    }

    internal DecimalPositivo Valor()
    {
        return Quantidade.Valor * Produto.Valor.Valor;
    }

    internal void Atualizar(InteiroPositivo quantidade, Produto produto, string observacao)
    {
        if(produto is null) throw new ArgumentException("O produto não pode ser nulo.", nameof(produto));
        
        Quantidade = quantidade;
        Produto = produto;
        Observacao = observacao;
    }
}
