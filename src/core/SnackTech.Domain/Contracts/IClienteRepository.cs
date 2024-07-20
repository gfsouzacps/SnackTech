using SnackTech.Domain.Models;

namespace SnackTech.Domain.Contracts
{
    public interface IClienteRepository
    {
        Task InserirCliente(Cliente novoCliente);
        Task<Cliente?> PesquisarPorCpf(string cpf);
        Task<Cliente> PesquisarClientePadrao();        
    }
}