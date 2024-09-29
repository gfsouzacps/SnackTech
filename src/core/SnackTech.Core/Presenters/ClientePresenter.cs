using SnackTech.Common.Dto.Api;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters;

internal class ClientePresenter
{
    internal static ResultadoOperacao<ClienteDto> ApresentarResultadoCliente(Cliente cliente)
    {
        ClienteDto clienteDto = cliente;
        return new ResultadoOperacao<ClienteDto>(clienteDto);
    }
}
