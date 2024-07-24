using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Pedido;
using SnackTech.Application.Interfaces;

namespace SnackTech.Application.UseCases
{
    public class PedidoService : IPedidoService
    {
        public Task<Result> AtualizarPedido(AtualizacaoPedido pedidoAtualizado)
        {
            throw new NotImplementedException();
        }

        public Task<Result<RetornoPedido>> BuscarPorIdenticacao(string identificacao)
        {
            throw new NotImplementedException();
        }

        public Task<Result<RetornoPedido>> BuscarUltimoPedidoCliente(string identificacaoCliente)
        {
            throw new NotImplementedException();
        }

        public Task<Result> FinalizarPedidoParaPagamento(string identificacao)
        {
            throw new NotImplementedException();
        }

        public Task<Result<Guid>> IniciarPedido(string identificacaoCliente)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<RetornoPedido>>> ListarPedidosParaPagamento()
        {
            throw new NotImplementedException();
        }
    }
}