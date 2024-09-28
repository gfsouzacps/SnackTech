using System;
using SnackTech.Common.Dto;

namespace SnackTech.Common.Interfaces.DataSources;

public interface IPedidoDataSource
{
    Task<bool> InserirPedidoAsync(PedidoDto pedidoDto);
}
