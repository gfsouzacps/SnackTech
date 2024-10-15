namespace SnackTech.Common.Dto.Api
{
    public class PedidoPagamentoDto
    {
        public Guid Id {get; set;}
        public string QrCode {get; set;} = default!;
        public decimal ValorTotal {get; set;}
    }
}