using System;
using SnackTech.Common.Interfaces.DataSources;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;

namespace SnackTech.Core.Gateways;

public class ClienteGateway(IClienteDataSource clienteDataSource)
{
    internal async Task<bool> CadastrarNovoCliente(Cliente entidade)
    {
        throw new NotImplementedException();
    }

    internal async Task<Cliente?> ProcurarClientePorCpf(CpfValido cpf)
    {
        throw new NotImplementedException();
    }

    internal async Task<Cliente?> ProcurarClientePorEmail(EmailValido emailCliente)
    {
        throw new NotImplementedException();
    }
}
