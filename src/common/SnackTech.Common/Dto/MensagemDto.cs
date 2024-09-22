namespace SnackTech.Common.Dto
{
    public class MensagemDto(string mensagem)
    {
        public string Mensagem { get; set; } = mensagem;
    }
}