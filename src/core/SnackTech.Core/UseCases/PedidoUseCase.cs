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

    internal static async Task<ResultadoOperacao<PedidoRetornoDto>> BuscarPorIdenticacao(string identificacao, PedidoGateway pedidoGateway)
    {
        try
        {
            var pedido = await pedidoGateway.PesquisarPorIdentificacao(identificacao);

            var retorno = pedido is null ?
                                GeralPresenter.ApresentarResultadoErroLogico<PedidoRetornoDto>($"Não foi possível localizar um pedido com identificação {identificacao}.") :
                                PedidoPresenter.ApresentarResultadoPedido(pedido);

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<PedidoRetornoDto>(ex);
        }
    }

    internal static async Task<ResultadoOperacao<PedidoRetornoDto>> BuscarUltimoPedidoCliente(string cpfCliente, PedidoGateway pedidoGateway, ClienteGateway clienteGateway)
    {
        try
        {
            var clienteResultado = await (cpfCliente is null
                ? ClienteUseCase.SelecionarClientePadrao(clienteGateway)
                : ClienteUseCase.PesquisarPorCpf(cpfCliente, clienteGateway));

            if (!clienteResultado.Sucesso)
            {
                return GeralPresenter.ApresentarResultadoErroLogico<PedidoRetornoDto>(clienteResultado.Mensagem);
            }

            var cliente = (Cliente)clienteResultado.RecuperarDados();
            var ultimosPedidos = await pedidoGateway.PesquisarPedidosPorCliente(cliente.Id);
            var ultimoPedido = ultimosPedidos.OrderBy(p => p.DataCriacao).LastOrDefault();

            var retorno = ultimoPedido is null ?
                                GeralPresenter.ApresentarResultadoErroLogico<PedidoRetornoDto>($"Não foi possível encontrar um pedido para o cliente com CPF {cpfCliente}.") :
                                PedidoPresenter.ApresentarResultadoPedido(ultimoPedido);

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<PedidoRetornoDto>(ex);
        }
    }

    internal static async Task<ResultadoOperacao<List<PedidoRetornoDto>>> ListarPedidosParaPagamento(PedidoGateway pedidoGateway)
    {
        try
        {
            var pedidos = await pedidoGateway.PesquisarPedidosPorStatus(StatusPedidoValido.AguardandoPagamento);

            var retorno = PedidoPresenter.ApresentarResultadoPedido(pedidos);

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<List<PedidoRetornoDto>>(ex);
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
                                GeralPresenter.ApresentarResultadoErroLogico($"Não foi possível finalizar para pagamento o pedido com identificação {identificacao}.");

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno(ex);
        }
    }

    internal static async Task<ResultadoOperacao<PedidoRetornoDto>> AtualizarItensPedido(PedidoAtualizacaoDto pedidoAtualizado, PedidoGateway pedidoGateway, ProdutoGateway produtoGateway)
    {
        try
        {
            var pedidoResultado = await BuscarPorIdenticacao(pedidoAtualizado.Id.ToString(), pedidoGateway);
            if (!pedidoResultado.Sucesso)
            {
                return GeralPresenter.ApresentarResultadoErroLogico<PedidoRetornoDto>(pedidoResultado.Mensagem);
            }

            var pedido = (Pedido)pedidoResultado.RecuperarDados();

            //remover itens do pedido que estejam ausentes no pedido atualizado
            pedido.Itens.RemoveAll(itemPedido => !pedidoAtualizado.Itens.Any(itemAtualizado => itemAtualizado.Id == itemPedido.Id));

            //validar itens do pedido atualizado
            List<PedidoItem> itensValidados = await validarItensPedido(pedidoAtualizado.Itens, produtoGateway);

            //atualizar os itens do pedido
            pedido.Itens = itensValidados;

            var foiAtualizado = await pedidoGateway.AtualizarItensDoPedido(pedido);

            var retorno = foiAtualizado ?
                PedidoPresenter.ApresentarResultadoPedido(pedido) :
                GeralPresenter.ApresentarResultadoErroLogico<PedidoRetornoDto>($"Não foi possível atualizar os itens do pedido com identificação {pedidoAtualizado.Id}.");

            return retorno;
        }
        catch (Exception ex)
        {
            return GeralPresenter.ApresentarResultadoErroInterno<PedidoRetornoDto>(ex);
        }
    }

    private static async Task<List<PedidoItem>> validarItensPedido(List<PedidoItemAtualizacaoDto> itens, ProdutoGateway produtoGateway)
    {
        var itensValidados = new List<PedidoItem>();

        foreach (var item in itens)
        {
            var produto = await produtoGateway.ProcurarProdutoPorIdentificacao(item.ProdutoId);

            if (produto is null)
            {
                throw new ArgumentException($"Não existe produto com identificação {item.ProdutoId}.");
            }

            Guid identificacaoItem = item.Id == null ? Guid.NewGuid() : new GuidValido(item.Id);
            itensValidados.Add(new PedidoItem(identificacaoItem, produto, item.Quantidade, item.Observacao));
        }

        return itensValidados;
    }
}
