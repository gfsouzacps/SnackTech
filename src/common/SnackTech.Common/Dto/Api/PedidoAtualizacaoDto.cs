namespace SnackTech.Common.Dto.Api;

public class PedidoAtualizacaoDto
{
    public string Identificacao { get; set; }
    public IEnumerable<PedidoItemAtualizacaoDto> PedidoItens { get; set; }
}
