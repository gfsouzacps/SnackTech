namespace SnackTech.Common.Dto;

public class PedidoRetornoDto
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public int Status { get; set; }
    public ClienteDto Cliente { get; set; }
    public List<PedidoItemRetornoDto> Itens { get; set; }
}
