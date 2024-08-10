namespace SnackTech.Domain.Ports.Driven
{
    public interface IPedidoRepository
    {
        Task InserirPedidoAsync(DTOs.Driven.PedidoDto novoPedido);
        //TODO: Separar m�todo AtualizarPedido em dois, um para atualizar apenas o pedido em si, e outro para atualizar apenas os itens do pedido.
        //Pois da forma como est�, atualizaremos toda a entidade de Pedido quando apenas queremos atualizar os itens do pedido.
        Task AtualizarPedidoAsync(DTOs.Driven.PedidoDto pedidoAtualizado);
        Task<IEnumerable<DTOs.Driven.PedidoDto>> PesquisarPedidosParaPagamentoAsync();
        Task<DTOs.Driven.PedidoDto?> PesquisarPorIdentificacaoAsync(Guid identificacao);
        Task<IEnumerable<DTOs.Driven.PedidoDto>> PesquisarPorClienteAsync(Guid identificacaoCliente);
    }
}
