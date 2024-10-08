using SnackTech.Domain.Common;
using SnackTech.Domain.DTOs.Driving.Cliente;

namespace SnackTech.Domain.Ports.Driving
{
    public interface IClienteService
    {
        Task<Result<RetornoCliente>> Cadastrar(CadastroCliente cadastroCliente);
        Task<Result<RetornoCliente>> IdentificarPorCpf(string cpf);
        Task<Result<RetornoCliente>> SelecionarClientePadrao();
    }
}