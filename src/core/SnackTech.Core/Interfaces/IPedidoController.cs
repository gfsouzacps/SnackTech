using SnackTech.Common.Dto;

namespace SnackTech.Core.Interfaces;

public interface IPedidoController
{
    Task<ResultadoOperacao<Guid>> IniciarPedido(string? cpfCliente);
    Task<ResultadoOperacao<PedidoDto>> BuscarPorIdenticacao(string identificacao);
    Task<ResultadoOperacao<PedidoDto>> BuscarUltimoPedidoCliente(string cpfCliente);
    Task<ResultadoOperacao<List<PedidoDto>>> ListarPedidosParaPagamento();
    Task<ResultadoOperacao> FinalizarPedidoParaPagamento(string identificacao);
    Task<ResultadoOperacao> AtualizarPedido(PedidoDto pedidoAtualizado);
}
