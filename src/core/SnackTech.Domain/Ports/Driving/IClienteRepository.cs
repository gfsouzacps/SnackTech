using SnackTech.Domain.Models;

namespace SnackTech.Domain.Ports.Driving
{
    public interface IClienteRepository
    {
        Task InserirClienteAsync(Cliente novoCliente);
        Task<Cliente?> PesquisarPorCpfAsync(string cpf);
        Task<Cliente> PesquisarClientePadraoAsync();        
    }
}