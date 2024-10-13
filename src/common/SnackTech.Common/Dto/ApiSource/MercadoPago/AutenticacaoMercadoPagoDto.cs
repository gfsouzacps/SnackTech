namespace SnackTech.Common.Dto.ApiSource.MercadoPago
{
    public class AutenticacaoMercadoPagoDto
    {
        public string TokenDeAcesso {get; set;} = default!;
        public int TempoExpiracao {get; set;}
        public string IdUsuario {get; set;} = default!;
    }
}