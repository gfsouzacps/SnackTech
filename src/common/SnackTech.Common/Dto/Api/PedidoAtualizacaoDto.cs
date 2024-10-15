namespace SnackTech.Common.Dto.Api;

public class PedidoAtualizacaoDto
{
    public string IdentificacaoPedido { get; set; }
    public IEnumerable<PedidoItemAtualizacaoDto> PedidoItens { get; set; }
}
