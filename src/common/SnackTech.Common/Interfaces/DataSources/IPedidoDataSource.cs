using System;
using SnackTech.Common.Dto;

namespace SnackTech.Common.Interfaces.DataSources;

public interface IPedidoDataSource
{
    Task<bool> InserirPedidoAsync(PedidoRetornoDto pedidoDto);
    Task<PedidoRetornoDto?> PesquisarPorIdentificacaoAsync(string identificacao);
    Task<IEnumerable<PedidoRetornoDto>> PesquisarPedidosPorClienteAsync(Guid clienteId);
    Task<IEnumerable<PedidoRetornoDto>> PesquisarPedidosPorStatusAsync(int valor);
    Task<bool> AtualizarPedidoAsync(PedidoRetornoDto pedidoDto);
    Task<bool> AtualizarItensDoPedidoAsync(PedidoRetornoDto pedidoDto);
}
