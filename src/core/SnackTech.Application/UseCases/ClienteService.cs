using Microsoft.Extensions.Logging;
using SnackTech.Application.Common;
using SnackTech.Application.DTOs.Cliente;
using SnackTech.Application.Interfaces;
using SnackTech.Domain.Contracts;
using SnackTech.Domain.Guards;
using SnackTech.Domain.Models;

namespace SnackTech.Application.UseCases
{
    public class ClienteService(ILogger<ClienteService> logger, IClienteRepository clienteRepository) : IClienteService
    {
        private readonly ILogger<ClienteService> logger = logger;
        private readonly IClienteRepository clienteRepository = clienteRepository;

        public async Task<Result<RetornoCliente>> Cadastrar(CadastroCliente cadastroCliente)
        {
            try{
                var novoCliente = new Cliente(cadastroCliente.Nome,cadastroCliente.Email,cadastroCliente.CPF);
                await clienteRepository.InserirCliente(novoCliente);
                var retorno = RetornoCliente.APartirDeCliente(novoCliente);
                return new Result<RetornoCliente>(retorno);
            }
            catch(ArgumentException aex){
                logger.LogWarning("ClienteService.Cadastrar - ArgumentException - {Message}",aex.Message);
                return new Result<RetornoCliente>(aex.Message,true); 
            }
            catch(Exception ex){
                logger.LogError(ex,"ClienteService.Cadastrar - Exception - {Message}",ex.Message);
                return new Result<RetornoCliente>(ex);
            }
        }

        public async Task<Result<RetornoCliente>> IdentificarPorCpf(string cpf)
        {
            try{
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
                var cliente = await clienteRepository.PesquisarPorCpf(cpf);

                if(cliente == null){
                    return new Result<RetornoCliente>($"{cpf} não encontrado.",true);
                }

                var retorno = RetornoCliente.APartirDeCliente(cliente);
                return new Result<RetornoCliente>(retorno);
            }
            catch(Exception ex){
                logger.LogError(ex,"ClienteService.IdentificartPorCpf - Exception - {Message}",ex.Message);
                return new Result<RetornoCliente>(ex); 
            }
        }

        public async Task<Result<Guid>> SelecionarClientePadrao()
        {
            try{
                var clientePadrao = await clienteRepository.PesquisarClientePadrao();
                var retorno = clientePadrao.RecuperarUid();
                return new Result<Guid>(retorno);
            }
            catch(Exception ex){
                logger.LogError(ex,"ClienteService.SelecionarClientePadrao - Exception - {Message}",ex.Message);
                return new Result<Guid>(ex);
            }
        }
    }
}