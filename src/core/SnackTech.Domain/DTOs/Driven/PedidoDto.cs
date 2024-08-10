using System.Collections.Generic;
using SnackTech.Domain.Enums;

namespace SnackTech.Domain.DTOs.Driven;

public class PedidoDto
{

    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public ClienteDto Cliente { get; set; }
    public StatusPedido Status { get; set; }        
    public IEnumerable<PedidoItemDto> Itens { get; set; }
}

