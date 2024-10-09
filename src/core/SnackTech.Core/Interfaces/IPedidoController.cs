using SnackTech.Common.Dto.Api;

namespace SnackTech.Core.Interfaces;

public interface IPedidoController
{
    Task<ResultadoOperacao<Guid>> IniciarPedido(string? cpfCliente);
    Task<ResultadoOperacao<PedidoRetornoDto>> BuscarPorIdenticacao(string identificacao);
    Task<ResultadoOperacao<PedidoRetornoDto>> BuscarUltimoPedidoCliente(string cpfCliente);
    Task<ResultadoOperacao<IEnumerable<PedidoRetornoDto>>> ListarPedidosParaPagamento();
    Task<ResultadoOperacao<PedidoPagamentoDto>> FinalizarPedidoParaPagamento(string identificacao);
    Task<ResultadoOperacao<PedidoRetornoDto>> AtualizarPedido(PedidoAtualizacaoDto pedidoAtualizado);
}
