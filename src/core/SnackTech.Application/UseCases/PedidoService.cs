using Microsoft.Extensions.Logging;
using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Pedido;
using SnackTech.Application.Interfaces;
using SnackTech.Domain.Contracts;
using SnackTech.Domain.Guards;

namespace SnackTech.Application.UseCases
{
    public class PedidoService(ILogger<PedidoService> logger, IPedidoRepository pedidoRepository) : BaseService(logger), IPedidoService
    {
        private readonly IPedidoRepository pedidoRepository = pedidoRepository;

        public Task<Result> AtualizarPedido(AtualizacaoPedido pedidoAtualizado)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<RetornoPedido>> BuscarPorIdenticacao(string identificacao)
        {
            async Task<Result<RetornoPedido>> processo()
            {
                var guid = CustomGuards.AgainstInvalidGuid(identificacao, nameof(identificacao));
                var pedido = await pedidoRepository.PesquisarPorId(guid);

                if (pedido is null)
                    return new Result<RetornoPedido>($"Pedido com identificação {identificacao} não encontrado.", true);

                var retorno = RetornoPedido.APartirDePedido(pedido);
                return new Result<RetornoPedido>(retorno);
            }
            return await CommonExecution($"PedidoService.BuscarPorIdenticacao {identificacao}", processo);
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
