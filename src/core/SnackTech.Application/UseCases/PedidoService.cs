using Microsoft.Extensions.Logging;
using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Pedido;
using SnackTech.Application.Interfaces;
using SnackTech.Domain.Contracts;
using SnackTech.Domain.Guards;
using SnackTech.Domain.Models;

namespace SnackTech.Application.UseCases
{
    public class PedidoService(ILogger<PedidoService> logger, IPedidoRepository pedidoRepository, IClienteRepository clienteRepository) : BaseService(logger), IPedidoService
    {
        private readonly IPedidoRepository pedidoRepository = pedidoRepository;
        private readonly IClienteRepository clienteRepository = clienteRepository;

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

        public async Task<Result<Guid>> IniciarPedido(string? cpfCliente)
        {
            async Task<Result<Guid>> processo()
            {
                Cliente? cliente;
                if(cpfCliente == null)
                {
                    cliente = await clienteRepository.PesquisarClientePadrao();
                }
                else
                {
                    CpfGuard.AgainstInvalidCpf(cpfCliente, nameof(cpfCliente));
                    cliente = await clienteRepository.PesquisarPorCpf(cpfCliente);
                }
                
                var novoPedido = new Pedido(cliente);
                await pedidoRepository.InserirPedido(novoPedido);

                return new Result<Guid>(novoPedido.Id);
            }
            return await CommonExecution($"PedidoService.IniciarPedido {cpfCliente}", processo);
        }

        public async Task<Result<IEnumerable<RetornoPedido>>> ListarPedidosParaPagamento()
        {
            async Task<Result<IEnumerable<RetornoPedido>>> processo()
            {
                var pedidos = await pedidoRepository.PesquisarPedidosParaPagamento();
                var retorno = pedidos.Select(RetornoPedido.APartirDePedido);
                return new Result<IEnumerable<RetornoPedido>>(retorno);
            }
            return await CommonExecution($"PedidoService.ListarPedidosParaPagamento", processo);
        }
    }
}
