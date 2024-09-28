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
                                GeralPresenter.ApresentarResultadoErroLogico<Guid>($"Não foi possível iniciar o pedido para o cliente com CPF {cpfCliente}.");

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<Guid>(ex);
        }
    }

    internal static async Task<ResultadoOperacao<PedidoDto>> BuscarPorIdenticacao(string identificacao, PedidoGateway pedidoGateway)
    {
        try
        {
            var pedido = await pedidoGateway.PesquisarPorIdentificacao(identificacao);

            var retorno = pedido is null ?
                                GeralPresenter.ApresentarResultadoErroLogico<PedidoDto>($"Não foi possível localizar um pedido com identificação {identificacao}.") :
                                PedidoPresenter.ApresentarResultadoPedido(pedido);

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<PedidoDto>(ex);
        }
    }

    internal static async Task<ResultadoOperacao<PedidoDto>> BuscarUltimoPedidoCliente(string cpfCliente, PedidoGateway pedidoGateway, ClienteGateway clienteGateway)
    {
        try
        {
            var clienteResultado = await (cpfCliente is null
                ? ClienteUseCase.SelecionarClientePadrao(clienteGateway)
                : ClienteUseCase.PesquisarPorCpf(cpfCliente, clienteGateway));

            if (!clienteResultado.Sucesso)
            {
                return GeralPresenter.ApresentarResultadoErroLogico<PedidoDto>(clienteResultado.Mensagem);
            }

            var cliente = (Cliente)clienteResultado.RecuperarDados();
            var ultimosPedidos = await pedidoGateway.PesquisarPedidosPorCliente(cliente.Id);
            var ultimoPedido = ultimosPedidos.OrderBy(p => p.DataCriacao).LastOrDefault();

            var retorno = ultimoPedido is null ?
                                GeralPresenter.ApresentarResultadoErroLogico<PedidoDto>($"Não foi possível encontrar um pedido para o cliente com CPF {cpfCliente}.") :
                                PedidoPresenter.ApresentarResultadoPedido(ultimoPedido);

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<PedidoDto>(ex);
        }
    }

    internal static async Task<ResultadoOperacao<List<PedidoDto>>> ListarPedidosParaPagamento(PedidoGateway pedidoGateway)
    {
        try
        {
            var pedidos = await pedidoGateway.PesquisarPedidosPorStatus(StatusPedidoValido.AguardandoPagamento);

            var retorno = PedidoPresenter.ApresentarResultadoPedido(pedidos);

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<List<PedidoDto>>(ex);
        }
    }

    internal static async Task<ResultadoOperacao> FinalizarPedidoParaPagamento(string identificacao, PedidoGateway pedidoGateway)
    {
        try
        {
            var pedidoResultado = await BuscarPorIdenticacao(identificacao, pedidoGateway);
            if (!pedidoResultado.Sucesso)
            {
                return GeralPresenter.ApresentarResultadoErroLogico(pedidoResultado.Mensagem);
            }

            var pedido = (Pedido)pedidoResultado.RecuperarDados();
            pedido.FecharPedidoParaPagamento();

            var foiAtualizado = await pedidoGateway.AtualizarPedido(pedido);

            var retorno = foiAtualizado ?
                                PedidoPresenter.ApresentarResultadoOk() :
                                GeralPresenter.ApresentarResultadoErroLogico($"Não foi possível finalizar para pagamento o pedido com identificação {identificacao}.") :

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno(ex);
        }
    }
}
