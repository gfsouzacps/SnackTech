using Microsoft.EntityFrameworkCore;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Adapter.DataBase.Entities;
using SnackTech.Adapter.DataBase.Util;
using SnackTech.Domain.Contracts;
using SnackTech.Domain.Enums;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class ProdutoRepository(RepositoryDbContext repositoryDbContext) : IProdutoRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task AlterarProdutoAsync(Domain.Models.Produto produtoAlterado)
        {
            var produtoEntity = Mapping.Mapper.Map<Produto>(produtoAlterado);
            
            _repositoryDbContext.Entry(produtoEntity).State = EntityState.Modified;
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task InserirProdutoAsync(Domain.Models.Produto novoProduto)
        {
            var produtoEntity = Mapping.Mapper.Map<Produto>(novoProduto);

            _repositoryDbContext.Add(produtoEntity);
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Models.Produto>> PesquisarPorCategoriaAsync(CategoriaProduto categoria)
        {
            var produtosBanco = await _repositoryDbContext.Produtos
                    .Where(p => p.Categoria == categoria)
                    .ToListAsync();

            return produtosBanco.Select(Mapping.Mapper.Map<Domain.Models.Produto>);
        }

        public async Task<Domain.Models.Produto?> PesquisarPorIdentificacaoAsync(Guid identificacao)
        {
            var produto = await _repositoryDbContext.Produtos
                .FirstOrDefaultAsync(p => p.Id == identificacao);

            return Mapping.Mapper.Map<Domain.Models.Produto>(produto);
        }

        public async Task<bool> RemoverProdutoPorIdentificacaoAsync(Guid identificacao)
        {
            var produto = await PesquisarPorIdentificacaoAsync(identificacao);

            if (produto is null)
                return false;

            var produtoEntity = Mapping.Mapper.Map<Produto>(produto);

            _repositoryDbContext.Produtos.Remove(produtoEntity);
            return await _repositoryDbContext.SaveChangesAsync() > 0;
        }
    }
}