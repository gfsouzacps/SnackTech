using Newtonsoft.Json;
using SnackTech.Common.Dto;
using SnackTech.Core.Domain.Entities;

namespace SnackTech.Core.Presenters
{
    internal static class ProdutoPresenter
    {
        internal static string ApresentarProdutoComoJson(Produto produto){
            var entidadeSerializada = JsonConvert.SerializeObject(produto);
            return entidadeSerializada;
        }

        internal static string ApresentarOperacaoSucessoSemValor(string mensagem){
            var retorno = new MensagemDto(mensagem);
            return JsonConvert.SerializeObject(retorno);
        }

        internal static string ApresentarListaProdutosComoJson(IEnumerable<Produto> produtos){
            var listaSerializada = JsonConvert.SerializeObject(produtos,Formatting.Indented);
            return listaSerializada;
        }
    }
}