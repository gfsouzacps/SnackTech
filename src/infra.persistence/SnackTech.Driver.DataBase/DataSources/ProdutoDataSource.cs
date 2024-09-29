using Microsoft.EntityFrameworkCore;
using SnackTech.Common.CustomExceptions;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Driver.DataBase.Context;
using SnackTech.Driver.DataBase.Entities;
using SnackTech.Driver.DataBase.Util;

namespace SnackTech.Driver.DataBase.DataSources
{
    public class ProdutoDataSource(RepositoryDbContext repositoryDbContext) : IProdutoDataSource
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;
        public async Task<bool> AlterarProdutoAsync(ProdutoDto produtoAlterado)
        {
            var produtoEntity = Mapping.Mapper.Map<Produto>(produtoAlterado);

            _repositoryDbContext.Entry(produtoEntity).State = EntityState.Modified;
            await _repositoryDbContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> InserirProdutoAsync(ProdutoDto produtoNovo)
        {
            var produtoEntity = Mapping.Mapper.Map<Produto>(produtoNovo);

            bool existe = await _repositoryDbContext.Produtos
                .AnyAsync(p => (int)p.Categoria == produtoNovo.Categoria && p.Nome == produtoNovo.Nome);
            if (existe)
                throw new ProdutoRepositoryException("Já existe um produto com o mesmo nome e categoria no sistema.");

            _repositoryDbContext.Add(produtoEntity);
            var resultado = await _repositoryDbContext.SaveChangesAsync();
            
            return resultado > 0;
        }

        public async Task<IEnumerable<ProdutoDto>> PesquisarPorCategoriaIdAsync(int categoriaId)
        {
            var produtosBanco = await _repositoryDbContext.Produtos
                   .AsNoTracking()
                   .Where(p => (int)p.Categoria == categoriaId)
                   .ToListAsync();

            return produtosBanco.Select(Mapping.Mapper.Map<ProdutoDto>);
        }

        public async Task<ProdutoDto?> PesquisarPorIdentificacaoAsync(Guid identificacao)
        {
            var produtoBanco = await _repositoryDbContext.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == identificacao);

            if(produtoBanco == null)
            {
                return null;
            }

            return Mapping.Mapper.Map<ProdutoDto>(produtoBanco);
        }

        public async Task<ProdutoDto?> PesquisarPorNomeAsync(string nome)
        {
            var produtoBanco = await _repositoryDbContext.Produtos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Nome == nome);

            if (produtoBanco == null)
            {
                return null;
            }

            return Mapping.Mapper.Map<ProdutoDto>(produtoBanco);
        }

        public async Task<bool> RemoverProdutoPorIdentificacaoAsync(Guid identificacao)
        {
            var produto = await _repositoryDbContext.Produtos
                 .FirstOrDefaultAsync(p => p.Id == identificacao);

            if (produto is null)
                return false;

            var existeItemAssociado = await _repositoryDbContext.PedidoItens
                .AnyAsync(p => p.Produto.Id == identificacao);

            if (existeItemAssociado)
                throw new ProdutoRepositoryException("Não foi possível remover o produto. Existem itens de pedidos associados a este produto.");

            _repositoryDbContext.Produtos.Remove(produto);
            var resultado = await _repositoryDbContext.SaveChangesAsync();
            
            return resultado > 0;
        }
    }
}