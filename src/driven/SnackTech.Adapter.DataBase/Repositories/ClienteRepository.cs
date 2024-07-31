using Microsoft.EntityFrameworkCore;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Domain.Contracts;
using SnackTech.Domain.Models;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class ClienteRepository(RepositoryDbContext repositoryDbContext) : IClienteRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task InserirClienteAsync(Cliente novoCliente)
        {
            _repositoryDbContext.Clientes.Add(novoCliente);
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task<Cliente> PesquisarClientePadraoAsync()
        {
            var cliente = await _repositoryDbContext.Clientes
                .FirstOrDefaultAsync(c => c.Cpf == Cliente.CPF_CLIENTE_PADRAO);

            if (cliente == null) {
                throw new Exception("Cliente padrão não foi localizado no banco de dados.");
            }

            return cliente; 
        }

        public async Task<Cliente?> PesquisarPorCpfAsync(string cpf)
        {
            return await _repositoryDbContext.Clientes
                .FirstOrDefaultAsync(c => c.Cpf == cpf);
        }
    }
}
