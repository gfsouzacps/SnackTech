using SnackTech.Domain.Models;

namespace SnackTech.Domain.Contracts
{
    public interface IPedidoRepository
    {
        Task InserirPedidoAsync(Pedido novoPedido);
        //TODO: Separar método AtualizarPedido em dois, um para atualizar apenas o pedido em si, e outro para atualizar apenas os itens do pedido.
        //Pois da forma como está, atualizaremos toda a entidade de Pedido quando apenas queremos atualizar os itens do pedido.
        Task AtualizarPedidoAsync(Pedido pedidoAtualizado);
        Task<IEnumerable<Pedido>> PesquisarPedidosParaPagamentoAsync();
        Task<Pedido?> PesquisarPorIdentificacaoAsync(Guid identificacao);
        Task<IEnumerable<Pedido>> PesquisarPorClienteAsync(Guid identificacaoCliente);
    }
}
