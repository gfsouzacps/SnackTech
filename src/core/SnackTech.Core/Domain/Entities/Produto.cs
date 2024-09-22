using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities
{
    //primary constructor. Feature do C#12. 
    //Explicita os valores obrigatórios na definição da classe
    internal class Produto(Guid id, CategoriaProdutoValido categoriaProduto, StringNaoVaziaOuComEspacos nome, StringNaoVazia descricao, DecimalPositivo valor)
    {
        internal Guid Id { get; private set; } = id;
        internal CategoriaProdutoValido Categoria { get; private set; } = categoriaProduto;
        internal StringNaoVaziaOuComEspacos Nome { get; private set; } = nome;
        internal StringNaoVazia Descricao { get; private set; } = descricao;
        internal DecimalPositivo Valor { get; private set; } = valor;

        internal void AlterarDados(ProdutoSemIdDto produtoDto){
            Categoria = produtoDto.Categoria;
            Nome = produtoDto.Nome;
            Descricao = produtoDto.Descricao;
            Valor = produtoDto.Valor;
        }

        public Produto(ProdutoDto produtoDto)
            :this(produtoDto.Id,
                    produtoDto.Categoria,
                    produtoDto.Nome,
                    produtoDto.Descricao,
                    produtoDto.Valor)
        {}

        public static implicit operator Produto(ProdutoDto produtoDto){
            return new Produto(produtoDto);
        }

        public static implicit operator ProdutoDto(Produto produto){
            return new ProdutoDto{
                Id = produto.Id,
                Categoria = produto.Categoria,
                Descricao = produto.Descricao,
                Nome = produto.Nome,
                Valor = produto.Valor
            };
        }
    }
}