using SnackTech.Common.Dto.Api;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters;

internal static class ClientePresenter
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
}
