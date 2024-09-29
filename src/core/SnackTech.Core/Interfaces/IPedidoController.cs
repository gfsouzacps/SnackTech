using SnackTech.Common.Dto.Api;
using SnackTech.Common.Dto.DataSource;

namespace SnackTech.Core.Interfaces;

public interface IPedidoController
{
    Task<ResultadoOperacao<Guid>> IniciarPedido(string? cpfCliente);
    Task<ResultadoOperacao<PedidoRetornoDto>> BuscarPorIdenticacao(string identificacao);
    Task<ResultadoOperacao<PedidoRetornoDto>> BuscarUltimoPedidoCliente(string cpfCliente);
    Task<ResultadoOperacao<List<PedidoRetornoDto>>> ListarPedidosParaPagamento();
    Task<ResultadoOperacao> FinalizarPedidoParaPagamento(string identificacao);
    Task<ResultadoOperacao<PedidoRetornoDto>> AtualizarPedido(PedidoAtualizacaoDto pedidoAtualizado);
}
