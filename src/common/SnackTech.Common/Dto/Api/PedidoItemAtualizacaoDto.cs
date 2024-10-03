namespace SnackTech.Common.Dto.Api;

public class PedidoItemAtualizacaoDto
{
    //Informar o Id caso seja uma atualiza��o do item ou null para item novo no pedido
    public string? Identificacao { get; set; }
    public int Quantidade { get; set; }
    public string Observacao { get; set; }
    public string IdentificacaoProduto { get; set; }
}
