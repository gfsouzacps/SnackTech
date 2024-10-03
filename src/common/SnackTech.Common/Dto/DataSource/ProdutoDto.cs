namespace SnackTech.Common.Dto.DataSource;

public class ProdutoDto
{
    public Guid Id { get; set; }
    public int Categoria { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}