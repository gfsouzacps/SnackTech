using SnackTech.Domain.Models;

namespace SnackTech.Domain.Contracts
{
    public interface IPedidoRepository
    {
        Task InserirPedido(Pedido novoPedido);
        Task AtualizarPedido(Pedido pedidoAtualizado);
        Task PesquisarPedidosParaPagamento();
        Task<Pedido?> PesquisarPorId(Guid identificacao);
        Task<IEnumerable<Pedido>> PesquisarPorCliente(string CPF);
    }
}
