using SnackTech.Common.Dto;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Gateways;

public class PedidoGateway(IPedidoDataSource dataSource)
{
    internal async Task<bool> CadastrarNovoPedido(Pedido entidade)
    {
        var pedidoDto = (PedidoRetornoDto)entidade;

        return await dataSource.InserirPedidoAsync(pedidoDto);
    }

    internal async Task<Pedido?> PesquisarPorIdentificacao(GuidValido identificacao)
    {
        var pedidoDto = await dataSource.PesquisarPorIdentificacaoAsync(identificacao);

        if (pedidoDto is null)
            return null;

        return (Pedido)pedidoDto;
    }


    internal async Task<IEnumerable<Pedido>> PesquisarPedidosPorCliente(GuidValido clienteId)
    {
        var pedidosDto = await dataSource.PesquisarPedidosPorClienteAsync(clienteId);

        return pedidosDto.Select(p => (Pedido)p);
    }

    internal async Task<IEnumerable<Pedido>> PesquisarPedidosPorStatus(StatusPedidoValido status)
    {
        var pedidosDto = await dataSource.PesquisarPedidosPorStatusAsync(status.Valor);

        return pedidosDto.Select(p => (Pedido)p);
    }

    internal async Task<bool> AtualizarPedido(Pedido pedido)
    {
        var pedidoDto = (PedidoRetornoDto)pedido;

        return await dataSource.AtualizarPedidoAsync(pedidoDto);
    }

    internal async Task<bool> AtualizarItensDoPedido(Pedido pedido)
    {
        var pedidoDto = (PedidoRetornoDto)pedido;

        return await dataSource.AtualizarItensDoPedidoAsync(pedidoDto);
    }
}
