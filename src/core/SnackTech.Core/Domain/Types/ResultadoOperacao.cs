namespace SnackTech.Core.Domain.Types
{
    public class ResultadoOperacao
    {
        public bool Sucesso {get; protected set;}
        public string Mensagem {get; protected set;}
        public Exception Excecao {get; protected set;}

        public ResultadoOperacao()
        {
            Sucesso = true;
            Mensagem = string.Empty;
            Excecao = null!;
        }

        public ResultadoOperacao(string message){
            Sucesso = false;
            Mensagem = message;
            Excecao = null!;
        }

        public ResultadoOperacao(Exception exception){
            Sucesso = false;
            Mensagem = exception.Message;
            Excecao = exception;
        }

        public bool TeveSucesso() => Sucesso;
        public bool TeveExcecao() => Excecao != null;
    }

    public class ResultadoOperacao<T> : ResultadoOperacao{
        public T Dados {get; protected set;}

        public ResultadoOperacao(T dados)
            :base()
        {
            Dados = dados;
            Sucesso = true;
        }

        public ResultadoOperacao(string mensagem, bool houveErro): base(mensagem){
            if(!houveErro){
                var mensagemExcecao = "Use ResultadoOperacao<string>(string) como construtor para resultados de sucesso.";
                throw new ArgumentException(mensagemExcecao,nameof(houveErro));
            }
            Dados = default!;
        }

        public ResultadoOperacao(Exception excecao)
            :base(excecao)
        {
            Dados = default!;
        }

        public T RecuperarDados() => Dados;
    }
}