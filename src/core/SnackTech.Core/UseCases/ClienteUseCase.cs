using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using SnackTech.Core.Gateways;
using SnackTech.Core.Presenters;

namespace SnackTech.Core.UseCases;

public static class ClienteUseCase
{
    internal static async Task<ResultadoOperacao<ClienteDto>> CriarNovoCliente(ClienteSemIdDto clienteDto, ClienteGateway clienteGateway)
    {
        try
        {
            //Criar entidade valida automaticamente os parametros
            var entidade = new Cliente(Guid.NewGuid(), clienteDto.Nome, clienteDto.Email, clienteDto.Cpf);

            var clienteCpfExistente = await clienteGateway.ProcurarClientePorCpf(entidade.Cpf);
            if (clienteCpfExistente != null)
            {
                return GeralPresenter.ApresentarResultadoErroLogico<ClienteDto>($"Cliente com CPF {clienteDto.Cpf} já cadastrado.");
            }

            var clienteEmailExistente = await clienteGateway.ProcurarClientePorEmail(entidade.Email);
            if (clienteEmailExistente != null)
            {
                return GeralPresenter.ApresentarResultadoErroLogico<ClienteDto>($"Cliente com email {clienteDto.Email} já cadastrado.");
            }

            var foiCadastrado = await clienteGateway.CadastrarNovoCliente(entidade);
            var retorno = foiCadastrado ?
                            ClientePresenter.ApresentarResultadoCliente(entidade) :
                            GeralPresenter.ApresentarResultadoErroLogico<ClienteDto>($"Não foi possível cadastrar cliente {entidade.Nome}.");

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<ClienteDto>(ex);
        }
    }

    internal static async Task<ResultadoOperacao<ClienteDto>> PesquisarPorCpf(string cpf, ClienteGateway gateway)
    {
        try
        {
            var cpfValidado = new CpfValido(cpf);
            var cliente = await gateway.ProcurarClientePorCpf(cpfValidado);
            var retorno = cliente != null ?
                            ClientePresenter.ApresentarResultadoCliente(cliente) :
                            GeralPresenter.ApresentarResultadoErroLogico<ClienteDto>($"Não foi possível encontrar um cliente com CPF {cpf}.");

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<ClienteDto>(ex);
        }
    }

    internal static async Task<ResultadoOperacao<ClienteDto>> SelecionarClientePadrao(ClienteGateway gateway)
    {
        try
        {
            var cliente = await gateway.ProcurarClientePorCpf(Cliente.CPF_CLIENTE_PADRAO);
            var retorno = cliente != null ?
                            ClientePresenter.ApresentarResultadoCliente(cliente) :
                            GeralPresenter.ApresentarResultadoErroLogico<ClienteDto>("Cliente padrão não encontrado.");

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<ClienteDto>(ex);
        }
    }
}
