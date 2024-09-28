using System;
using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using SnackTech.Core.Gateways;
using SnackTech.Core.Presenters;

namespace SnackTech.Core.UseCases;

internal static class PedidoUseCase
{
    internal static async Task<ResultadoOperacao<Guid>> IniciarPedido(string? cpfCliente, PedidoGateway pedidoGateway, ClienteGateway clienteGateway)
    {
        try
        {
            var clienteResultado = await (cpfCliente is null
                ? ClienteUseCase.SelecionarClientePadrao(clienteGateway)
                : ClienteUseCase.PesquisarPorCpf(cpfCliente, clienteGateway));

            if (!clienteResultado.Sucesso)
            {
                return GeralPresenter.ApresentarResultadoErroLogico<Guid>(clienteResultado.Mensagem);
            }

            var cliente = (Cliente)clienteResultado.RecuperarDados();
            var entidade = new Pedido(Guid.NewGuid(), DateTime.Now, StatusPedidoValido.Iniciado, cliente);

            var foiCadastrado = await pedidoGateway.CadastrarNovoPedido(entidade);
            var retorno = foiCadastrado ?
                                PedidoPresenter.ApresentarResultadoPedidoIniciado(entidade) :
                                GeralPresenter.ApresentarResultadoErroLogico<Guid>($"Não foi possível iniciar o pedido para o cliente com CPF{cpfCliente}.");

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<Guid>(ex);
        }
    }
}
