namespace SnackTech.Core.Domain.Types
{
    internal class ResultadoOperacao
    {
        public bool Sucesso {get; protected set;}
        public string Mensagem {get; protected set;}
        public Exception Excecao {get; protected set;}

        internal ResultadoOperacao()
        {
            Sucesso = true;
            Mensagem = string.Empty;
            Excecao = null!;
        }

        internal ResultadoOperacao(string message){
            Sucesso = false;
            Mensagem = message;
            Excecao = null!;
        }

        internal ResultadoOperacao(Exception exception){
            Sucesso = false;
            Mensagem = exception.Message;
            Excecao = exception;
        }

        internal bool TeveSucesso() => Sucesso;
        internal bool TeveExcecao() => Excecao != null;
    }

    internal class ResultadoOperacao<T> : ResultadoOperacao{
        public T Dados {get; protected set;}

        internal ResultadoOperacao(T dados)
            :base()
        {
            Dados = dados;
            Sucesso = true;
        }

        internal ResultadoOperacao(string mensagem, bool houveErro): base(mensagem){
            if(!houveErro){
                var mensagemExcecao = "Use ResultadoOperacao<string>(string) como construtor para resultados de sucesso.";
                throw new ArgumentException(mensagemExcecao,nameof(houveErro));
            }
            Dados = default!;
        }

        internal ResultadoOperacao(Exception excecao)
            :base(excecao)
        {
            Dados = default!;
        }

        internal T RecuperarDados() => Dados;
    }
}