using SnackTech.Common.Dto.Api;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters;

internal class ClientePresenter
{
    internal static ResultadoOperacao<ClienteDto> ApresentarResultadoCliente(Cliente cliente)
    {
        ClienteDto clienteDto = ConverterParaDto(cliente);
        return new ResultadoOperacao<ClienteDto>(clienteDto);
    }

    internal static ClienteDto ConverterParaDto(Cliente cliente)
    {
        return new ClienteDto
        {
            Id = cliente.Id,
            Nome = cliente.Nome.Valor,
            Email = cliente.Email.Valor,
            Cpf = cliente.Cpf.Valor
        };
    }

    internal static Cliente ConverterParaEntidade(ClienteDto clienteDto)
    {
        return new Cliente(clienteDto.Id, clienteDto.Nome, clienteDto.Email, clienteDto.Cpf);
    }
}
