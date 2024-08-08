using Microsoft.EntityFrameworkCore;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;
using SnackTech.Domain.Ports.Driven;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class ProdutoRepository(RepositoryDbContext repositoryDbContext) : IProdutoRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task AlterarProdutoAsync(Produto produtoAlterado)
        {
            _repositoryDbContext.Entry(produtoAlterado).State = EntityState.Modified;
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task InserirProdutoAsync(Produto novoProduto)
        {
            _repositoryDbContext.Add(novoProduto);
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> PesquisarPorCategoriaAsync(CategoriaProduto categoria)
        {
            return await _repositoryDbContext.Produtos
                    .AsNoTracking()
                    .Where(p => p.Categoria == categoria)
                    .ToListAsync();
        }

        public async Task<Produto?> PesquisarPorIdentificacaoAsync(Guid identificacao)
        {
            return await _repositoryDbContext.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == identificacao);
        }

        public async Task<bool> RemoverProdutoPorIdentificacaoAsync(Guid identificacao)
        {
            var produto = await PesquisarPorIdentificacaoAsync(identificacao);

            if (produto is null)
                return false;

            _repositoryDbContext.Produtos.Remove(produto);
            return await _repositoryDbContext.SaveChangesAsync() > 0;
        }
    }
}