using SnackTech.Domain.Contracts;
using SnackTech.Domain.Models;

namespace SnackTech.Adapter.DataBase.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        public Task AtualizarPedido(Pedido pedidoAtualizado)
        {
            throw new NotImplementedException();
        }

        public Task InserirPedido(Pedido novoPedido)
        {
            throw new NotImplementedException();
        }

        public Task PesquisarPedidosParaPagamento()
        {
            throw new NotImplementedException();
        }

        public Task PesquisarPorCliente(string CPF)
        {
            throw new NotImplementedException();
        }

        Task<Pedido?> IPedidoRepository.PesquisarPorId(Guid identificacao)
        {
            throw new NotImplementedException();
        }
    }
}