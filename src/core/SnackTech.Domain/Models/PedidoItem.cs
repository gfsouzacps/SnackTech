
using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Models
{
    public class PedidoItem
    {
        private decimal _valor;

        public Guid Id {get; private set;}        
        public int Sequencial {get; private set;}
        public int Quantidade {get; private set;}
        public string Observacao {get; private set;}
        public decimal Valor {
            get { return _valor; }
        }
        public Guid IdProduto { get; private set; }
        public Produto Produto { get; private set; } = null!;
        public Guid IdPedido { get; private set; }
        public Pedido? Pedido { get; private set; }

        public PedidoItem(Guid id, Guid idPedido, int sequencial, Guid idProduto, int quantidade, string observacao)
        {
            CustomGuards.AgainstObjectNull(idPedido, nameof(idPedido));
            CustomGuards.AgainstObjectNull(idProduto, nameof(idProduto));
            CustomGuards.AgainstNegativeOrZeroValue(quantidade, nameof(quantidade));
            CustomGuards.AgainstNegativeOrZeroValue(sequencial, nameof(sequencial));

            Id = id;
            IdPedido = idPedido;
            Sequencial = sequencial;
            IdProduto = idProduto;
            Quantidade = quantidade;
            Observacao = PreencherObservacao(observacao);
        }

        public PedidoItem(Guid idPedido, int sequencial, Produto produto, int quantidade, string observacao)
            :this(Guid.NewGuid(), idPedido, sequencial, produto.Id, quantidade, observacao)
        {
            CustomGuards.AgainstObjectNull(produto, nameof(produto));

            Produto = produto;
            CalcularValor();
        }

        public void AtualizarDadosItem(Produto produto, int quantidade, string observacao){
            CustomGuards.AgainstObjectNull(produto, nameof(produto));
            CustomGuards.AgainstNegativeOrZeroValue(quantidade, nameof(quantidade));
            Produto = produto;
            Quantidade = quantidade;
            CalcularValor();
            Observacao = PreencherObservacao(observacao);
        }

        private void CalcularValor()
        {
            _valor = Quantidade * Produto.Valor;
        }

        private static string PreencherObservacao(string observacao)
            => observacao ?? string.Empty;
    }
}