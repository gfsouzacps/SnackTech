using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Cliente;

namespace SnackTech.Application.Interfaces
{
    public interface IClienteService
    {
        /*
            CadastrarCliente
                Receber nome, email e cpf
                Gravar cliente
                Retornar id do cliente (será usado para vincular cliente ao pedido)
            IdentificarClientePeloCPF
                Receber cpf
                Procurar cliente pelo CPF
                Retornar id do cliente (será usado para vincular cliente ao pedido)
            BuscarClienteAnonimo
                Retornar Identificação padrão que será usado em casos onde a pessoa não quer se identificar
        */
        Task<Result<RetornoCliente>> Cadastrar(CadastroCliente cadastroCliente);
        Task<Result<RetornoCliente>> IdentificarPorCpf(string cpf);
        Task<Result<Guid>> SelecionarClientePadrao();
    }
}