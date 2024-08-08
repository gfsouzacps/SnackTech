using Microsoft.EntityFrameworkCore;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Domain.Models;
using SnackTech.Domain.Ports.Driven;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class ClienteRepository(RepositoryDbContext repositoryDbContext) : IClienteRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task InserirClienteAsync(Cliente novoCliente)
        {
            var clienteExistente = _repositoryDbContext.Clientes
                .FirstOrDefaultAsync(clienteBd => clienteBd.Cpf == novoCliente.Cpf || clienteBd.Email == novoCliente.Email);

            if (clienteExistente != null)
            {
                throw new Exception("Já existe cliente cadastrado com esse email ou CPF.");
            }

            _repositoryDbContext.Clientes.Add(novoCliente);
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task<Cliente> PesquisarClientePadraoAsync()
        {
            var cliente = await _repositoryDbContext.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Cpf == Cliente.CPF_CLIENTE_PADRAO);

            if (cliente == null)
            {
                throw new Exception("Cliente padrão não foi localizado no banco de dados.");
            }

            return cliente;
        }

        public async Task<Cliente?> PesquisarPorCpfAsync(string cpf)
        {
            return await _repositoryDbContext.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Cpf == cpf);
        }
    }
}
