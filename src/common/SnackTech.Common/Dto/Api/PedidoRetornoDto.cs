namespace SnackTech.Common.Dto.Api;

public class PedidoRetornoDto
{
    public Guid IdentificacaoPedido { get; set; }
    public DateTime DataCriacao { get; set; }
    public int Status { get; set; }
    public ClienteDto Cliente { get; set; }
    public IEnumerable<PedidoItemRetornoDto> Itens { get; set; }
}
