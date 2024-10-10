using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Gateways;

internal class ProdutoGateway(IProdutoDataSource dataSource)
{       
    internal async Task<Produto?> ProcurarProdutoPorNome(StringNaoVaziaOuComEspacos nome){
        var produtoDto = await dataSource.PesquisarPorNomeAsync(nome.ToString());

        if(produtoDto == null || produtoDto.Id == Guid.Empty){
            return null;
        }

        return ConverterParaEntidade(produtoDto);
    }     

    internal async Task<Produto?> ProcurarProdutoPorIdentificacao(GuidValido id){
        var produtoDto = await dataSource.PesquisarPorIdentificacaoAsync(id);

        if(produtoDto == null || produtoDto.Id == Guid.Empty){
            return null;
        }

        return ConverterParaEntidade(produtoDto);
    }

    internal async Task<bool> CadastrarNovoProduto(Produto novoProduto){            
        ProdutoDto dto = ConverterParaDto(novoProduto);

        return await dataSource.InserirProdutoAsync(dto);
    }

    internal async Task<bool> AtualizarProduto(Produto produtoAlterado){
        ProdutoDto dto = ConverterParaDto(produtoAlterado);

        return await dataSource.AlterarProdutoAsync(dto);
    }

    internal async Task<bool> RemoverProduto(GuidValido id){
        return await dataSource.RemoverProdutoPorIdentificacaoAsync(id);
    }

    internal async Task<IEnumerable<Produto>> ProcurarProdutosPorCategoria(CategoriaProdutoValido categoriaProduto){
        var produtosDto = await dataSource.PesquisarPorCategoriaIdAsync(categoriaProduto);

        return produtosDto.Select(ConverterParaEntidade);
    }

    internal static Produto ConverterParaEntidade(ProdutoDto produtoDto)
    {
        return new Produto(
            produtoDto.Id,
            produtoDto.Categoria,
            produtoDto.Nome,
            produtoDto.Descricao,
            produtoDto.Valor
        );
    }

    internal static ProdutoDto ConverterParaDto(Produto produto)
    {
        return new ProdutoDto
        {
            Id = produto.Id,
            Categoria = produto.Categoria.Valor,
            Nome = produto.Nome.Valor,
            Descricao = produto.Descricao,
            Valor = produto.Valor.Valor
        };
    }
}