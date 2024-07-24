using Microsoft.Extensions.Logging;
using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Pedido;
using SnackTech.Application.Interfaces;
using SnackTech.Domain.Contracts;
using SnackTech.Domain.Guards;
using SnackTech.Domain.Models;

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

        public async Task<Result<RetornoPedido>> BuscarUltimoPedidoCliente(string cpfCliente)
        {
            async Task<Result<RetornoPedido>> processo()
            {
                CpfGuard.AgainstInvalidCpf(cpfCliente, nameof(cpfCliente));
                IEnumerable<Pedido> pedidos = await pedidoRepository.PesquisarPorCliente(cpfCliente);
                var ultimoPedido = pedidos.OrderBy(p => p.DataCriacao).LastOrDefault();

                if (ultimoPedido is null)
                    return new Result<RetornoPedido>($"Último Pedido do cliente com cpf {cpfCliente} não encontrado.", true);

                var retorno = RetornoPedido.APartirDePedido(ultimoPedido);
                return new Result<RetornoPedido>(retorno);
            }
            return await CommonExecution($"PedidoService.BuscarUltimoPedidoCliente {cpfCliente}", processo);
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
