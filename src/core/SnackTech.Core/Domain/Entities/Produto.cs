using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities;

internal class Produto(GuidValido id, CategoriaProdutoValido categoriaProduto, StringNaoVaziaOuComEspacos nome, StringNaoVazia descricao, DecimalPositivo valor)
{
    internal GuidValido Id { get; private set; } = id;
    internal CategoriaProdutoValido Categoria { get; private set; } = categoriaProduto;
    internal StringNaoVaziaOuComEspacos Nome { get; private set; } = nome;
    internal StringNaoVazia Descricao { get; private set; } = descricao;
    internal DecimalPositivo Valor { get; private set; } = valor;

    internal void AlterarDados(Common.Dto.Api.ProdutoSemIdDto produtoDto){
        Categoria = produtoDto.Categoria;
        Nome = produtoDto.Nome;
        Descricao = produtoDto.Descricao;
        Valor = produtoDto.Valor;
    }

    public Produto(Common.Dto.Api.ProdutoDto produtoDto)
        :this(produtoDto.Id,
                produtoDto.Categoria,
                produtoDto.Nome,
                produtoDto.Descricao,
                produtoDto.Valor)
    {}
    public Produto(Common.Dto.DataSource.ProdutoDto produtoDto)
        : this(produtoDto.Id,
                produtoDto.Categoria,
                produtoDto.Nome,
                produtoDto.Descricao,
                produtoDto.Valor)
    { }

    public static implicit operator Produto(Common.Dto.Api.ProdutoDto produtoDto){
        return new Produto(produtoDto);
    }

    public static implicit operator Produto(Common.Dto.DataSource.ProdutoDto produtoDto)
    {
        return new Produto(produtoDto);
    }

    public static implicit operator Common.Dto.Api.ProdutoDto(Produto produto){
        return new Common.Dto.Api.ProdutoDto
        {
            Id = produto.Id,
            Categoria = produto.Categoria,
            Descricao = produto.Descricao,
            Nome = produto.Nome,
            Valor = produto.Valor
        };
    }

    public static implicit operator Common.Dto.DataSource.ProdutoDto(Produto produto)
    {
        return new Common.Dto.DataSource.ProdutoDto
        {
            Id = produto.Id,
            Categoria = produto.Categoria,
            Descricao = produto.Descricao,
            Nome = produto.Nome,
            Valor = produto.Valor
        };
    }
}