namespace SnackTech.Core.Common.Dto
{
    public class MensagemDto(string mensagem)
    {
        public string Mensagem { get; set; } = mensagem;
    }
}