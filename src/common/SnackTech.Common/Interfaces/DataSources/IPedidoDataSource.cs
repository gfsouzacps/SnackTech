using SnackTech.Common.Dto.DataSource;

namespace SnackTech.Common.Interfaces.DataSources;

public interface IPedidoDataSource
{
    Task<bool> InserirPedidoAsync(PedidoDto pedidoDto);
    Task<PedidoDto?> PesquisarPorIdentificacaoAsync(Guid identificacao);
    Task<IEnumerable<PedidoDto>> PesquisarPedidosPorClienteIdAsync(Guid clienteId);
    Task<IEnumerable<PedidoDto>> PesquisarPedidosPorStatusAsync(int valor);
    Task<bool> AlterarItensDoPedidoAsync(PedidoDto pedidoDto);
    Task<bool> AlterarPedidoAsync(PedidoDto pedidoDto);
}
