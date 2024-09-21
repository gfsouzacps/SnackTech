using Microsoft.Extensions.Logging;
using SnackTech.Domain.Common;
using SnackTech.Domain.DTOs.Driving.Cliente;
using SnackTech.Domain.Guards;
using SnackTech.Domain.Entities;
using SnackTech.Domain.Ports.Driven;
using SnackTech.Domain.Ports.Driving;

namespace SnackTech.Application.UseCases
{
    public class ClienteService(ILogger<ClienteService> logger, IClienteRepository clienteRepository) : BaseService(logger),IClienteService
    {
        private readonly IClienteRepository clienteRepository = clienteRepository;

        public async Task<Result<RetornoCliente>> Cadastrar(CadastroCliente cadastroCliente)
        {
            async Task<Result<RetornoCliente>> processo(){
                var novoCliente = new Cliente(cadastroCliente.Nome,cadastroCliente.Email,cadastroCliente.CPF);
                var clienteDto = await clienteRepository.PesquisarPorCpfAsync(novoCliente.Cpf);
                
                if(clienteDto is not null){
                    return new Result<RetornoCliente>($"{novoCliente.Cpf} já foi cadastrado.",true);
                }
                
                await clienteRepository.InserirClienteAsync((Domain.DTOs.Driven.ClienteDto)novoCliente);
                var retorno = RetornoCliente.APartirDeCliente(novoCliente);
                return new Result<RetornoCliente>(retorno);
            }
            return await CommonExecution("ClienteService.Cadastrar",processo);
        }

        public async Task<Result<RetornoCliente>> IdentificarPorCpf(string cpf)
        {
            async Task<Result<RetornoCliente>> processo(){
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
                var clienteDto = await clienteRepository.PesquisarPorCpfAsync(cpf);

                if(clienteDto == null){
                    return new Result<RetornoCliente>($"{cpf} não encontrado.",true);
                }

                var retorno = RetornoCliente.APartirDeCliente((Cliente)clienteDto);
                return new Result<RetornoCliente>(retorno);
            }
            return await CommonExecution($"ClienteService.IdentificarPorCpf - {cpf}",processo);
        }

        public async Task<Result<RetornoCliente>> SelecionarClientePadrao()
        {
            async Task<Result<RetornoCliente>> processo(){
                var clientePadrao = await clienteRepository.PesquisarClientePadraoAsync();
                var retorno = RetornoCliente.APartirDeCliente((Cliente)clientePadrao);
                return new Result<RetornoCliente>(retorno);
            }
            return await CommonExecution("ClienteService.SelecionarClientePadrao",processo);
        }
    }
}