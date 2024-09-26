using System;
using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters;

public class ClientePresenter
{
    internal static ResultadoOperacao<ClienteDto> ApresentarResultadoCliente(Cliente cliente){
        ClienteDto clienteDto = cliente;
        return new ResultadoOperacao<ClienteDto>(clienteDto);
    }
}
