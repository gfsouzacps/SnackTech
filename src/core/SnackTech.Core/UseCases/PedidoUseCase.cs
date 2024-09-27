using System;

namespace SnackTech.Core.UseCases;

internal static class PedidoUseCase
{
    internal static async Task<ResultadoOperacao<Guid>> IniciarPedido(string? cpfCliente, PedidoGateway pedidoGateway, ClienteGateway clienteGateway)
    {
        try
        {
            var cpfValido = new CpfValido(cpfCliente);
            ResultadoOperacao<ClienteDto> clienteResultado;

            if (cpfCliente == null)
            {
                clienteResultado = await ClienteUseCase.SelecionarClientePadrao(clienteGateway);
            }
            else
            {
                clienteResultado = await ClienteUseCase.PesquisarPorCpf(cpfCliente, clienteGateway)
            }

            if (!clienteResultado.Sucesso)
            {
                return GeralPresenter.ApresentarResultadoErroLogico<Guid>(clienteResultado.Mensagem);
            }

            var clienteDto = clienteResultado.RecuperarDados();

            var novoPedido = new Pedido((Cliente)clienteDto);

        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<Guid>(ex);
        }
    }
}
