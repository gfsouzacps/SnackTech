
using SnackTech.Common.Dto.Api;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using SnackTech.Core.Gateways;
using SnackTech.Core.Presenters;

namespace SnackTech.Core.UseCases
{
    internal static class PagamentoUseCase
    {
        internal static async Task<ResultadoOperacao> ProcessarPagamentoEnviadoPorHook(PedidoGateway pedidoGateway, MercadoPagoGateway mercadoPagoGateway, PagamentoDto pagamento){
            try{
                //validar status do pagamento
                    //se for de abertura, só retorna sucesso por enquanto
                ActionPagamentoValido acao = pagamento.action;
                if(acao == "create")
                    return GeralPresenter.ApresentarResultadoPadraoSucesso();

                //validar que status está closed. Se estiver diferente não fazer nenhuma operação
                if(pagamento.status != "closed")
                    return GeralPresenter.ApresentarResultadoPadraoSucesso();


                //buscar order no MP
                var referencia = await mercadoPagoGateway.BuscarPedidoViaOrder(pagamento.id);
                //se achar, procurar pedido na nossa base de acordo com referencia externa
                var pedido = await pedidoGateway.PesquisarPorIdentificacao(referencia);

                if(pedido is null)
                    return GeralPresenter.ApresentarResultadoErroLogico($"Pedido {referencia} não encontrado para atualizar status após pagamento");

                //se achar, atualiza status do pedido para prosseguir
                pedido.AtualizarPedidoAposPagamento();

                var foiAtualizado = await pedidoGateway.AtualizarStatusPedido(pedido);

                var retorno = foiAtualizado ? 
                                    GeralPresenter.ApresentarResultadoPadraoSucesso():
                                    GeralPresenter.ApresentarResultadoErroLogico($"Não foi possível atualizar pedido {pedido.Id} após pagamento");
                return retorno;
            }
            catch(ArgumentException ex){
                return GeralPresenter.ApresentarResultadoErroLogico(ex.Message);
            }
            catch(Exception ex){
                return GeralPresenter.ApresentarResultadoErroInterno(ex);
            }
        }

        internal static async Task<ResultadoOperacao> ProcessarPagamentoViaMock(PedidoGateway pedidoGateway,Guid identificacaoPedido){
            try{
                var pedido = await pedidoGateway.PesquisarPorIdentificacao(identificacaoPedido);

            if(pedido is null)
                return GeralPresenter.ApresentarResultadoErroLogico($"Pedido {identificacaoPedido} não encontrado para atualizar status.");

            pedido.AtualizarPedidoAposPagamento();

            var foiAtualizado = await pedidoGateway.AtualizarStatusPedido(pedido);

            var retorno = foiAtualizado?
                                GeralPresenter.ApresentarResultadoPadraoSucesso():
                                GeralPresenter.ApresentarResultadoErroLogico($"Não foi possível atualizar pedido {pedido.Id} após pagamento");
            return retorno;
            }
             catch(ArgumentException ex){
                return GeralPresenter.ApresentarResultadoErroLogico(ex.Message);
            }
            catch(Exception ex){
                return GeralPresenter.ApresentarResultadoErroInterno(ex);
            }
        }
    }
}