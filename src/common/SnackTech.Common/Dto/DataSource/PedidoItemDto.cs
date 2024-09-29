using SnackTech.Common.Dto.Api;

namespace SnackTech.Common.Dto.DataSource;

public class PedidoItemDto
{
    public Guid Id { get; set; }
    public int Quantidade { get; set; }
    public decimal Valor { get; set; }
    public string Observacao { get; set; }
    public ProdutoDto Produto { get; set; }
}
