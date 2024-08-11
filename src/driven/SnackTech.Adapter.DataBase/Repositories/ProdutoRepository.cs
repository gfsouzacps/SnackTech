using Microsoft.EntityFrameworkCore;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Adapter.DataBase.Entities;
using SnackTech.Adapter.DataBase.Util;
using SnackTech.Domain.Enums;
using SnackTech.Domain.Ports.Driven;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class ProdutoRepository(RepositoryDbContext repositoryDbContext) : IProdutoRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task AlterarProdutoAsync(Domain.DTOs.Driven.ProdutoDto produtoAlterado)
        {
            var produtoEntity = Mapping.Mapper.Map<Produto>(produtoAlterado);
            
            _repositoryDbContext.Entry(produtoEntity).State = EntityState.Modified;
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task InserirProdutoAsync(Domain.DTOs.Driven.ProdutoDto novoProduto)
        {
            var produtoEntity = Mapping.Mapper.Map<Produto>(novoProduto);

            _repositoryDbContext.Add(produtoEntity);
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.DTOs.Driven.ProdutoDto>> PesquisarPorCategoriaAsync(CategoriaProduto categoria)
        {
            var produtosBanco = await _repositoryDbContext.Produtos
                    .AsNoTracking()
                    .Where(p => p.Categoria == categoria)
                    .ToListAsync();

            return produtosBanco.Select(Mapping.Mapper.Map<Domain.DTOs.Driven.ProdutoDto>);
        }

        public async Task<Domain.DTOs.Driven.ProdutoDto?> PesquisarPorIdentificacaoAsync(Guid identificacao)
        {
            var produto = await _repositoryDbContext.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == identificacao);

            return Mapping.Mapper.Map<Domain.DTOs.Driven.ProdutoDto>(produto);
        }

        public async Task<bool> RemoverProdutoPorIdentificacaoAsync(Guid identificacao)
        {
            var produto = await _repositoryDbContext.Produtos
                .FirstOrDefaultAsync(p => p.Id == identificacao);

            if (produto is null)
                return false;

            _repositoryDbContext.Produtos.Remove(produto);
            return await _repositoryDbContext.SaveChangesAsync() > 0;
        }
    }
}