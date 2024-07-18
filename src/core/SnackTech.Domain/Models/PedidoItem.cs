
using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Models
{
    public class PedidoItem
    {
        public Guid Id {get; private set;}        
        public int Sequencial {get; private set;}
        public Produto Produto {get; private set;}
        public int Quantidade {get; private set;}
        public string Observacao {get; private set;}
        public decimal Valor => Quantidade * Produto.Valor;
        public Guid IdPedido { get; private set; }
        public Pedido? Pedido { get; private set; }

        public PedidoItem(Guid id, Guid idPedido, int sequencial, Produto produto, int quantidade, string observacao)
        {
            CustomGuards.AgainstObjectNull(idPedido, nameof(idPedido));
            CustomGuards.AgainstObjectNull(produto, nameof(produto));
            CustomGuards.AgainstNegativeOrZeroValue(quantidade, nameof(quantidade));
            CustomGuards.AgainstNegativeOrZeroValue(sequencial, nameof(sequencial));

            Id = id;
            Produto = produto;
            Quantidade = quantidade;
            Sequencial = sequencial;
            Observacao = PreencherObservacao(observacao);
            IdPedido = idPedido;
        }

        public PedidoItem(Guid idPedido, int sequencial, Produto produto, int quantidade, string observacao)
            :this(Guid.NewGuid(), idPedido, sequencial, produto, quantidade, observacao)
        {}

        public void AtualizarDadosItem(Produto produto, int quantidade, string observacao){
            CustomGuards.AgainstObjectNull(produto, nameof(produto));
            CustomGuards.AgainstNegativeOrZeroValue(quantidade, nameof(quantidade));
            Produto = produto;
            Quantidade = quantidade;
            Observacao = PreencherObservacao(observacao);
        }

        private static string PreencherObservacao(string observacao)
            => observacao ?? string.Empty;
    }
}