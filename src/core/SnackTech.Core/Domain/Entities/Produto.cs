using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class Produto(GuidValido id, CategoriaProdutoValido categoriaProduto, StringNaoVaziaOuComEspacos nome, string descricao, DecimalPositivo valor)
{
    internal GuidValido Id { get; private set; } = id;
    internal CategoriaProdutoValido Categoria { get; private set; } = categoriaProduto;
    internal StringNaoVaziaOuComEspacos Nome { get; private set; } = nome;
    internal string Descricao { get; private set; } = descricao;
    internal DecimalPositivo Valor { get; private set; } = valor;

    internal void Atualizar(CategoriaProdutoValido categoriaProduto, StringNaoVaziaOuComEspacos nome, string descricao, DecimalPositivo valor)
    {
        this.Categoria = categoriaProduto;
        this.Nome = nome;
        this.Descricao = descricao;
        this.Valor = valor;
    }
}