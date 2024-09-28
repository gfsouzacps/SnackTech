namespace SnackTech.Common.Dto;

public class PedidoItemRetornoDto
{
    public Guid Id { get; set; }
    public int Quantidade { get; set; }
    public decimal Valor { get; set; }
    public string Observacao { get; set; }
    public ProdutoDto Produto { get; set; }
}
