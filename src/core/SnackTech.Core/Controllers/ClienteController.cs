using System;
using SnackTech.Common.Dto;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Gateways;
using SnackTech.Core.UseCases;

namespace SnackTech.Core.Controllers;

public class ClienteController(IClienteDataSource clienteDataSource)
{
    public async Task<ResultadoOperacao<ClienteDto>> CadastrarNovoProduto(ClienteSemIdDto clienteSemIdDto)
    {
        var gateway = new ClienteGateway(clienteDataSource);

        var novoCliente = await ClienteUseCase.CriarNovoCliente(clienteSemIdDto, gateway);

        return novoCliente;
    }
}
