using Newtonsoft.Json;
using SnackTech.Core.Common.Dto;

namespace SnackTech.Core.Presenters
{
    public static class ErroPresenter
    {
        public static string ApresentarErroDeClienteComoJson(string mensagem){
            var objetoRetorno = new MensagemDto(mensagem);
            return JsonConvert.SerializeObject(objetoRetorno);
        }

        public static string ApresentarErroInternoComoJson(string mensagem,Exception excecao){
            var excecaoRetorno = new ExcecaoRetorno(excecao);
            var excecaoInterna = new ErroInternoDto(mensagem,excecaoRetorno);
            return JsonConvert.SerializeObject(excecaoInterna);
        }
    }
}