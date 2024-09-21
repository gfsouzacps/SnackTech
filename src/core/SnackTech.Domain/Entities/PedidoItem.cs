
using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Entities
{
    public class PedidoItem
    {
        private decimal _valor;

        public Guid Id {get; internal set;}        
        public int Sequencial {get; internal set;}
        public int Quantidade {get; internal set;}
        public string Observacao {get; internal set;}
        public decimal Valor {
            get { return _valor; }
        }
        public Produto Produto { get; internal set; }
        public Pedido Pedido { get; internal set; }

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

        internal PedidoItem() { }

        public void AtualizarDadosItem(Produto produto, int quantidade, string observacao){
            CustomGuards.AgainstObjectNull(produto, nameof(produto));
            CustomGuards.AgainstNegativeOrZeroValue(quantidade, nameof(quantidade));
            Produto = produto;
            Quantidade = quantidade;
            CalcularValor();
            Observacao = PreencherObservacao(observacao);
        }

        internal void CalcularValor()
        {
            _valor = Quantidade * Produto.Valor;
        }

        private static string PreencherObservacao(string observacao)
            => observacao ?? string.Empty;

        public static implicit operator DTOs.Driven.PedidoItemDto(PedidoItem pedidoItem)
        {
            return new DTOs.Driven.PedidoItemDto
            {
                Id = pedidoItem.Id,
                Sequencial = pedidoItem.Sequencial,
                Quantidade = pedidoItem.Quantidade,
                Observacao = pedidoItem.Observacao,
                Valor = pedidoItem.Valor,
                Produto = (DTOs.Driven.ProdutoDto)pedidoItem.Produto,
                Pedido = (DTOs.Driven.PedidoDto)pedidoItem.Pedido
            };
        }

        public static implicit operator PedidoItem(DTOs.Driven.PedidoItemDto pedidoItemDto)
        {
            return new PedidoItem {
                Id = pedidoItemDto.Id,
                Sequencial = pedidoItemDto.Sequencial,
                Quantidade = pedidoItemDto.Quantidade,
                Observacao = pedidoItemDto.Observacao,
                _valor = pedidoItemDto.Valor,
                Produto = (Produto)pedidoItemDto.Produto,
                Pedido = (Pedido)pedidoItemDto.Pedido
            };
        }
    }
}