using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Cliente;
using SnackTech.Application.Interfaces;

namespace SnackTech.Application.UseCases
{
    public class ClienteService : IClienteService
    {
        public Task<Result<RetornoCliente>> Cadastrar(CadastroCliente cadastroCliente)
        {
            throw new NotImplementedException();
        }

        public Task<Result<RetornoCliente>> IdentificarPorCpf(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Guid>> SelecionarClientePadrao()
        {
            throw new NotImplementedException();
        }
    }
}