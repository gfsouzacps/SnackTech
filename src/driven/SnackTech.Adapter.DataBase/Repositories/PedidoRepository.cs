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

        public Task<IEnumerable<Pedido>> PesquisarPorCliente(string CPF)
        {
            throw new NotImplementedException();
        }

        public Task<Pedido?> PesquisarPorId(Guid identificacao)
        {
            throw new NotImplementedException();
        }
    }
}