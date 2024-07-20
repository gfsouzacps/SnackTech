using SnackTech.Domain.Contracts;
using SnackTech.Domain.Models;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        public Task InserirCliente(Cliente novoCliente)
        {
            throw new NotImplementedException();
        }

        public Task<Cliente> PesquisarClientePadrao()
        {
            throw new NotImplementedException();
        }

        public Task<Cliente?> PesquisarPorCpf(string cpf)
        {
            throw new NotImplementedException();
        }
    }
}