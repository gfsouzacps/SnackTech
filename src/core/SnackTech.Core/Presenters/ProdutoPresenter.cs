using Newtonsoft.Json;
using SnackTech.Core.Common.Dto;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters
{
    public static class ProdutoPresenter
    {
        public static string ApresentarProdutoComoJson(Produto produto){
            var entidadeSerializada = JsonConvert.SerializeObject(produto);
            return entidadeSerializada;
        }

        public static string ApresentarOperacaoSucessoSemValor(string mensagem){
            var retorno = new MensagemDto(mensagem);
            return JsonConvert.SerializeObject(retorno);
        }

        public static string ApresentarListaProdutosComoJson(IEnumerable<Produto> produtos){
            var listaSerializada = JsonConvert.SerializeObject(produtos,Formatting.Indented);
            return listaSerializada;
        }
    }
}