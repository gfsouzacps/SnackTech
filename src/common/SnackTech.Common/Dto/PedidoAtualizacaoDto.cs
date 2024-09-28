namespace SnackTech.Common.Dto;

public class PedidoAtualizacaoDto
{
    public string Id { get; set; }
    public List<PedidoItemAtualizacaoDto> Itens { get; set; }
}
