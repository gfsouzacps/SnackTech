using SnackTech.Domain.Models;

namespace SnackTech.Domain.Contracts
{
    public interface IClienteRepository
    {
        Task InserirCliente(Cliente novoCliente);
        Task PesquisarPorCpf(string cpf);
        Task PesquisarClientePadrao();        
    }
}