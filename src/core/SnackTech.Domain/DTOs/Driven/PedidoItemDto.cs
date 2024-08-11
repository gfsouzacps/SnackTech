
namespace SnackTech.Domain.DTOs.Driven;

public class PedidoItemDto
{
    public Guid Id {get; set;}        
    public int Sequencial {get; set;}   
    public int Quantidade {get; set;}   
    public string Observacao {get; set;} = string.Empty; 
    public decimal Valor {get; set;}  
    public ProdutoDto Produto {get; set;}
    public PedidoDto Pedido {get; set;}   

}
