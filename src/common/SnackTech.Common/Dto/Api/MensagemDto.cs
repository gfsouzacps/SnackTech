namespace SnackTech.Common.Dto.Api
{
    public class MensagemDto(string mensagem)
    {
        public string Mensagem { get; set; } = mensagem;
    }
}