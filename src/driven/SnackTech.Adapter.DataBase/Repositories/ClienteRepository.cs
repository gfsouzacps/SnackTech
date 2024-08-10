using Microsoft.EntityFrameworkCore;
using SnackTech.Adapter.DataBase.Context;
using SnackTech.Adapter.DataBase.Entities;
using SnackTech.Adapter.DataBase.Util;
using SnackTech.Domain.Ports.Driven;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class ClienteRepository(RepositoryDbContext repositoryDbContext) : IClienteRepository
    {
        private readonly RepositoryDbContext _repositoryDbContext = repositoryDbContext;

        public async Task InserirClienteAsync(Domain.DTOs.Driven.ClienteDto novoCliente)
        {
            var clienteExistente = _repositoryDbContext.Clientes
                .FirstOrDefaultAsync(clienteBd => clienteBd.Cpf == novoCliente.Cpf || clienteBd.Email == novoCliente.Email);

            if (clienteExistente != null)
            {
                throw new Exception("Já existe cliente cadastrado com esse email ou CPF.");
            }

            var clienteAdd = Mapping.Mapper.Map<Cliente>(novoCliente);

            _repositoryDbContext.Clientes.Add(clienteAdd);
            await _repositoryDbContext.SaveChangesAsync();
        }

        public async Task<Domain.DTOs.Driven.ClienteDto> PesquisarClientePadraoAsync()
        {
            var cliente = await _repositoryDbContext.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Cpf == Domain.Models.Cliente.CPF_CLIENTE_PADRAO);

            if (cliente == null)
            {
                throw new Exception("Cliente padrão não foi localizado no banco de dados.");
            }

            var clienteRetorno = Mapping.Mapper.Map<Domain.DTOs.Driven.ClienteDto>(cliente);

            return clienteRetorno;
        }

        public async Task<Domain.DTOs.Driven.ClienteDto?> PesquisarPorCpfAsync(string cpf)
        {
            var cliente = await _repositoryDbContext.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Cpf == cpf);

            return Mapping.Mapper.Map<Domain.DTOs.Driven.ClienteDto>(cliente);
        }
    }
}
