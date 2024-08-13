
namespace SnackTech.Domain.Ports.Driven
{
    public interface IPedidoRepository
    {
        Task InserirPedidoAsync(DTOs.Driven.PedidoDto novoPedido);
        Task AtualizarPedidoAsync(DTOs.Driven.PedidoDto pedidoAtualizado);
        Task<IEnumerable<DTOs.Driven.PedidoDto>> PesquisarPedidosParaPagamentoAsync();
        Task<DTOs.Driven.PedidoDto?> PesquisarPorIdentificacaoAsync(Guid identificacao);
        Task<IEnumerable<DTOs.Driven.PedidoDto>> PesquisarPorClienteAsync(Guid identificacaoCliente);
    }
}
