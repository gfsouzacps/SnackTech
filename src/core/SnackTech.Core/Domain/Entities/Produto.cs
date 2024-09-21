using SnackTech.Core.Common.Dto;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Domain.Entities
{
    //primary constructor. Feature do C#12. 
    //Explicita os valores obrigatórios na definição da classe
    public class Produto(Guid id, CategoriaProdutoValido categoriaProduto, StringNaoVaziaOuComEspacos nome, StringNaoVazia descricao, DecimalPositivo valor)
    {
        public Guid Id { get; private set; } = id;
        public CategoriaProdutoValido Categoria { get; private set; } = categoriaProduto;
        public StringNaoVaziaOuComEspacos Nome { get; private set; } = nome;
        public StringNaoVazia Descricao { get; private set; } = descricao;
        public DecimalPositivo Valor { get; private set; } = valor;

        public void AlterarDados(ProdutoDto produtoDto){
            Categoria = produtoDto.Categoria;
            Nome = produtoDto.Nome;
            Descricao = produtoDto.Descricao;
            Valor = produtoDto.Valor;
        }
    }
}