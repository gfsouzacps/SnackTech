namespace SnackTech.Core.Common.Dto
{

    public record ErroInternoDto (string Message, ExcecaoRetorno? Exception);

    public class ExcecaoRetorno(Exception excecao)
    {
        public string? Tipo { get; private set; } = excecao.GetType().FullName;
        public string? Stack { get; private set; } = excecao.StackTrace;
        public string? TargetSite { get; private set; } = excecao.TargetSite?.ToString();
    }
}