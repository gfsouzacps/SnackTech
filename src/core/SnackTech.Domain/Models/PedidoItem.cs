
using SnackTech.Domain.DTOs.Driven;
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
        public Produto Produto { get; private set; }
        public Pedido Pedido { get; private set; }

        public PedidoItem(Guid id, Pedido pedido, int sequencial, Produto produto, int quantidade, string observacao)
        {
            CustomGuards.AgainstObjectNull(pedido, nameof(pedido));
            CustomGuards.AgainstObjectNull(produto, nameof(produto));
            CustomGuards.AgainstNegativeOrZeroValue(quantidade, nameof(quantidade));
            CustomGuards.AgainstNegativeOrZeroValue(sequencial, nameof(sequencial));

            Id = id;
            Pedido = pedido;
            Sequencial = sequencial;
            Produto = produto;
            Quantidade = quantidade;
            Observacao = PreencherObservacao(observacao);
        }

        public PedidoItem(Pedido pedido, int sequencial, Produto produto, int quantidade, string observacao)
            :this(Guid.NewGuid(), pedido, sequencial, produto, quantidade, observacao)
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

        public static implicit operator PedidoItemDto(PedidoItem pedidoItem)
        {
            return new PedidoItemDto
            {
                Id = pedidoItem.Id,
                Sequencial = pedidoItem.Sequencial,
                Quantidade = pedidoItem.Quantidade,
                Observacao = pedidoItem.Observacao,
                Valor = pedidoItem.Valor,
                Produto = (ProdutoDto)pedidoItem.Produto,
                Pedido = (PedidoDto)pedidoItem.Pedido
            };
        }

        public static implicit operator PedidoItem(PedidoItemDto pedidoItem)
        {
            return new PedidoItem(pedidoItem.Id, (Pedido)pedidoItem.Pedido, pedidoItem.Sequencial, (Produto)pedidoItem.Produto, pedidoItem.Quantidade, pedidoItem.Observacao);
        }
    }
}