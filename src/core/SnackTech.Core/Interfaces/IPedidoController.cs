using SnackTech.Common.Dto;

namespace SnackTech.Core.Interfaces;

public interface IPedidoController
{
    Task<ResultadoOperacao<Guid>> IniciarPedido(string? cpfCliente);
}
