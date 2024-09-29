using SnackTech.Common.Dto.DataSource;

namespace SnackTech.Common.Interfaces.DataSources;

public interface IPedidoDataSource
{
    Task<bool> InserirPedidoAsync(PedidoDto pedidoDto);
    Task<PedidoDto?> PesquisarPorIdentificacaoAsync(string identificacao);
    Task<IEnumerable<PedidoDto>> PesquisarPedidosPorClienteAsync(Guid clienteId);
    Task<IEnumerable<PedidoDto>> PesquisarPedidosPorStatusAsync(int valor);
    Task<bool> AtualizarPedidoAsync(PedidoDto pedidoDto);
    Task<bool> AtualizarItensDoPedidoAsync(PedidoDto pedidoDto);
}
