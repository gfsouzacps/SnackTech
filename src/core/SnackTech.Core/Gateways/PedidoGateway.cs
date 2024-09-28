using System;
using SnackTech.Common.Dto;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Gateways;

public class PedidoGateway(IPedidoDataSource dataSource)
{
    internal async Task<bool> CadastrarNovoPedido(Pedido entidade)
    {
        var pedidoDto = (PedidoDto)entidade;

        return await dataSource.InserirPedidoAsync(pedidoDto);
    }
}
