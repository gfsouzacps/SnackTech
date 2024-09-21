namespace SnackTech.Core.Common.Dto
{

    public record ErroInternoDto (string Message, ExcecaoRetorno? Exception);

    public class ExcecaoRetorno{
        public string? Tipo {get; private set;}
        public string? Stack {get; private set;}
        public string? TargetSite {get; private set;}

        public ExcecaoRetorno(Exception excecao){
            Tipo = excecao.GetType().FullName;
            Stack = excecao.StackTrace;
            TargetSite = excecao.TargetSite?.ToString();
        }
    }
}