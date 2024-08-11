using SnackTech.Domain.Enums;
using SnackTech.Domain.Guards;
using System.Collections.ObjectModel;

namespace SnackTech.Domain.Models
{
    public class Pedido
    {
        private decimal _valor;
        private readonly List<PedidoItem> _itens = new();

        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Cliente Cliente { get; private set; }
        public IReadOnlyCollection<PedidoItem> Itens => _itens.AsReadOnly();
        public StatusPedido Status { get; private set; }
        public decimal Valor
        {
            get { return _valor; }
        }

        public Pedido(Guid id, DateTime dataCriacao, StatusPedido status, Cliente cliente)
        {
            CustomGuards.AgainstObjectNull(cliente, nameof(cliente));

            Id = id;
            DataCriacao = dataCriacao;
            Status = status;
            Cliente = cliente;
        }

        public Pedido(Cliente cliente)
            : this(Guid.NewGuid(), DateTime.Now, StatusPedido.Iniciado, cliente)
        { }

        protected Pedido() { }

        public void AdicionarItem(Produto produto, int quantidade, string observacao)
        {
            CustomGuards.AgainstObjectNull(produto, nameof(produto));
            CustomGuards.AgainstNegativeOrZeroValue(quantidade, nameof(quantidade));
            var novoSequencial = ProximoSequencial();
            var pedidoItem = new PedidoItem(this, novoSequencial, produto, quantidade, observacao);
            _itens.Add(pedidoItem);

            CalcularValorTotal();
        }

        public bool RemoverItemPorSequencial(int sequencial)
        {
            if (_itens.Any(i => i.Sequencial == sequencial))
            {
                var itemARemover = _itens.First(i => i.Sequencial == sequencial);
                _itens.Remove(itemARemover);

                CalcularValorTotal();

                return true;
            }

            return false;
        }

        public bool AtualizarItemPorSequencial(int sequencial, Produto produto, int quantidade, string observacao)
        {
            if (_itens.Any(i => i.Sequencial == sequencial))
            {
                var itemDaLista = _itens.First(i => i.Sequencial == sequencial);
                itemDaLista.AtualizarDadosItem(produto, quantidade, observacao);

                CalcularValorTotal();

                return true;
            }

            return false;
        }

        public void FecharPedidoParaPagamento()
        {
            Status = StatusPedido.AguardandoPagamento;
        }

        private void CalcularValorTotal()
        {
            _valor = _itens.Sum(i => i.Valor);
            //TODO: Adição de taxa/imposto ?
        }

        private int ProximoSequencial()
        {
            if (Itens.Count == 0)
            {
                return 1;
            }
            int ultimoSequencial = _itens.Max(i => i.Sequencial);
            return ultimoSequencial + 1;
        }

        public static implicit operator DTOs.Driven.PedidoDto(Pedido pedido)
        {
            var pedidoDto = new DTOs.Driven.PedidoDto
            {
                Id = pedido.Id,
                DataCriacao = pedido.DataCriacao,
                Status = pedido.Status,
                Cliente = (DTOs.Driven.ClienteDto)pedido.Cliente,
            };

            var pedidosItems = pedido.Itens.Select(i => 
                new DTOs.Driven.PedidoItemDto{
                    Sequencial = i.Sequencial,
                    Observacao = i.Observacao,
                    Quantidade = i.Quantidade,
                    Valor = i.Valor,
                    Produto = (DTOs.Driven.ProdutoDto)i.Produto,
                    Pedido = pedidoDto
                }
            ).ToList();

            pedidoDto.Itens = pedidosItems;
            
            return pedidoDto;
        }

        public static implicit operator Pedido(DTOs.Driven.PedidoDto pedidoDto)
        {
            var pedidoModel = new Pedido {
                Id = pedidoDto.Id,
                DataCriacao = pedidoDto.DataCriacao,
                Status = pedidoDto.Status,
                Cliente = (Cliente)pedidoDto.Cliente
            };
            
            List<PedidoItem> itensPedido = pedidoDto.Itens.Select(i => {
                var item = new PedidoItem(){
                    Sequencial = i.Sequencial,
                    Observacao = i.Observacao,
                    Quantidade = i.Quantidade,
                    Produto = (Produto)i.Produto,
                    Pedido = pedidoModel
                };
                item.CalcularValor();
                
                return item;
            }).ToList();
            
            if(itensPedido.Count > 0) pedidoModel._itens.AddRange(itensPedido);
            
            pedidoModel.CalcularValorTotal();

            return pedidoModel;
        }
    }
}
