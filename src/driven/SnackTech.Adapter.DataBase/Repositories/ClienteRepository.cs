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
            _repositoryDbContext.Clientes.Add(novoCliente);
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task<Cliente> PesquisarClientePadraoAsync()
        {
            //TODO: Encontrar uma melhor forma de definir o cliente padr�o. Por hora, criamos um cliente com esse ID hard coded
            var idClientePadrao = "6ee54a46-007f-4e4c-9fe8-1a13eadf7fd1";
            var guid = Guid.Parse(idClientePadrao);
            return await _repositoryDbContext.Clientes
                .FirstOrDefaultAsync(c => c.Id == guid);
        }

        public async Task<Cliente?> PesquisarPorCpfAsync(string cpf)
        {
            return await _repositoryDbContext.Clientes
                .FirstOrDefaultAsync(c => c.Cpf == cpf);
        }
    }
}