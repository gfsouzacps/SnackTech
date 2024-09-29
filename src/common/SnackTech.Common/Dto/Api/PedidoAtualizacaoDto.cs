namespace SnackTech.Common.Dto.Api;

public class PedidoAtualizacaoDto
{
    public string Id { get; set; }
    public IEnumerable<PedidoItemAtualizacaoDto> Itens { get; set; }
}
