
namespace SnackTech.Domain.Ports.Driven
{
    public interface IClienteRepository
    {
        Task InserirClienteAsync(DTOs.Driven.ClienteDto novoCliente);
        Task<DTOs.Driven.ClienteDto?> PesquisarPorCpfAsync(string cpf);
        Task<DTOs.Driven.ClienteDto> PesquisarClientePadraoAsync();        
    }
}