using System;

namespace SnackTech.Common.Dto;

public class PedidoDto
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public int Status { get; set; }
    public ClienteDto Cliente { get; set; }
    public List<PedidoItemDto> Itens { get; set; }
}
