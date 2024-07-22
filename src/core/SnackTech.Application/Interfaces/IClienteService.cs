using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Cliente;

namespace SnackTech.Application.Interfaces
{
    public interface IClienteService
    {
        Task<Result<RetornoCliente>> Cadastrar(CadastroCliente cadastroCliente);
        Task<Result<RetornoCliente>> IdentificarPorCpf(string cpf);
        Task<Result<Guid>> SelecionarClientePadrao();
    }
}