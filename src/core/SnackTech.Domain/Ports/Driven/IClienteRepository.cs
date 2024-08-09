using SnackTech.Domain.Models;

namespace SnackTech.Domain.Ports.Driven
{
    public interface IClienteRepository
    {
        Task InserirClienteAsync(Cliente novoCliente);
        Task<Cliente?> PesquisarPorCpfAsync(string cpf);
        Task<Cliente> PesquisarClientePadraoAsync();        
    }
}