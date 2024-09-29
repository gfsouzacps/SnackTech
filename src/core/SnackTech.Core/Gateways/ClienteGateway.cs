using SnackTech.Common.Dto.DataSource;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Gateways;

internal class ClienteGateway(IClienteDataSource dataSource)
{
    internal async Task<bool> CadastrarNovoCliente(Cliente entidade)
    {
        ClienteDto dto = entidade;

        return await dataSource.InserirClienteAsync(dto);
    }

    internal async Task<Cliente?> ProcurarClientePorCpf(CpfValido cpf)
    {
        var clienteDto = await dataSource.PesquisarPorCpfAsync(cpf);

        if (clienteDto == null)
        {
            return null;
        }

        return new Cliente(clienteDto);
    }

    internal async Task<Cliente?> ProcurarClientePorEmail(EmailValido emailCliente)
    {
        var clienteDto = await dataSource.PesquisarPorCpfAsync(emailCliente);

        if (clienteDto == null)
        {
            return null;
        }

        return new Cliente(clienteDto);
    }

    internal async Task<Cliente?> ProcurarClientePorIdentificacao(GuidValido identificacao)
    {
        var clienteDto = await dataSource.PesquisarPorIdAsync(identificacao);

        if (clienteDto == null)
        {
            return null;
        }

        return new Cliente(clienteDto);
    }
}
