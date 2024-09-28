using SnackTech.Common.Dto;

namespace SnackTech.Core.Interfaces;

public interface IClienteController
{
    Task<ResultadoOperacao<ClienteDto>> CadastrarNovoCliente(ClienteSemIdDto clienteSemIdDto);
    Task<ResultadoOperacao<ClienteDto>> IdentificarPorCpf(string cpf);
    Task<ResultadoOperacao<ClienteDto>> SelecionarClientePadrao();
}
