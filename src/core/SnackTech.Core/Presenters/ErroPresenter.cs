using Newtonsoft.Json;
using SnackTech.Common.Dto;

namespace SnackTech.Core.Presenters
{
    internal static class ErroPresenter
    {
        internal static string ApresentarErroDeClienteComoJson(string mensagem){
            var objetoRetorno = new MensagemDto(mensagem);
            return JsonConvert.SerializeObject(objetoRetorno);
        }

        internal static string ApresentarErroInternoComoJson(string mensagem,Exception excecao){
            var excecaoRetorno = new ExcecaoRetorno(excecao);
            var excecaoInterna = new ErroInternoDto(mensagem,excecaoRetorno);
            return JsonConvert.SerializeObject(excecaoInterna);
        }
    }
}