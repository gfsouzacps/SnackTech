
using Microsoft.EntityFrameworkCore;
using SnackTech.Common.CustomExceptions;
using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Driver.DataBase.Context;
using SnackTech.Driver.DataBase.Entities;
using SnackTech.Driver.DataBase.Util;

namespace SnackTech.Driver.DataBase.DataSources;

public class ClienteDataSource(RepositoryDbContext repositoryDbContext) : IClienteDataSource
{
    public async Task<bool> InserirClienteAsync(ClienteDto clienteNovo)
    {
        var clienteEntity = Mapping.Mapper.Map<Cliente>(clienteNovo);

        bool existe = await repositoryDbContext.Clientes
            .AnyAsync(c => c.Cpf == clienteNovo.Cpf && c.Email == clienteNovo.Email);

        if (existe)
            throw new ProdutoRepositoryException("Já existe um cliente com o mesmo cpf e email no sistema.");

        repositoryDbContext.Add(clienteEntity);
        var result = await repositoryDbContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<ClienteDto?> PesquisarPorCpfAsync(string cpf)
    {
        var clienteBanco = await repositoryDbContext.Clientes
                    .AsNoTracking()
                    .Where(c => c.Cpf == cpf)
                    .FirstOrDefaultAsync();

        if (clienteBanco is null)
        {
            return null;
        }

        return Mapping.Mapper.Map<ClienteDto>(clienteBanco);
    }

    public async Task<ClienteDto?> PesquisarPorEmailAsync(string email)
    {
        var clienteBanco = await repositoryDbContext.Clientes
                    .AsNoTracking()
                    .Where(c => c.Email == email)
                    .FirstOrDefaultAsync();

        if (clienteBanco is null)
        {
            return null;
        }

        return Mapping.Mapper.Map<ClienteDto>(clienteBanco);
    }

    public async Task<ClienteDto?> PesquisarPorIdAsync(Guid identificacao)
    {
        var clienteBanco = await repositoryDbContext.Clientes
                    .AsNoTracking()
                    .Where(c => c.Id == identificacao)
                    .FirstOrDefaultAsync();
        
        if (clienteBanco is null)
        {
            return null;
        }

        return Mapping.Mapper.Map<ClienteDto>(clienteBanco);
    }
}
