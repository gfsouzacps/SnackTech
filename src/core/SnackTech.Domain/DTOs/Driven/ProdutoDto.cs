
using SnackTech.Domain.Enums;

namespace SnackTech.Domain.DTOs.Driven;

public class ProdutoDto
{
    public Guid Id {get; set;}
    public CategoriaProduto Categoria {get; set;}
    public string Nome {get; set;} = string.Empty;
    public string Descricao {get; set;} = string.Empty; 
    public decimal Valor {get; set;}
}
