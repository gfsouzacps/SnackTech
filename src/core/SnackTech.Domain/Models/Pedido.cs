using SnackTech.Domain.Enums;
using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Models
{
    public class Pedido
    {
        public Guid Id {get; private set;}        
        public DateTime DataCriacao {get; private set;}
        public Cliente Cliente {get; private set;}
        public IList<PedidoItem> Itens {get; private set;}
        public StatusPedido Status {get; private set;}
        public decimal Valor => CalcularValorTotal();
        public Pedido(Guid id, DateTime dataCriacao,StatusPedido statusPedido, Cliente cliente, IList<PedidoItem> itens){
            CustomGuards.AgainstObjectNull(cliente, nameof(cliente));

            Id = id;
            DataCriacao = dataCriacao;
            Status = statusPedido;
            Cliente = cliente;
            Itens = itens;
        }

        public Pedido(Cliente cliente)
            :this(Guid.NewGuid(),DateTime.Now,StatusPedido.Iniciado,cliente,new List<PedidoItem>())
        {}

        public Pedido(Cliente cliente, IList<PedidoItem> itens)
            :this(Guid.NewGuid(),DateTime.Now,StatusPedido.Iniciado,cliente,itens)
        {}

        public void AdicionarItem(Produto produto, int quantidade, string observacao){
            CustomGuards.AgainstObjectNull(produto, nameof(produto));
            CustomGuards.AgainstNegativeOrZeroValue(quantidade,nameof(quantidade));
            var novoSequencial = ProximoSequencial();
            var pedidoItem = new PedidoItem(novoSequencial,produto,quantidade,observacao);
            Itens.Add(pedidoItem);
        }

        public bool RemoverItemPorSequencial(int sequencial){
            if(Itens.Any(i => i.Sequencial == sequencial)){
                var itemARemover = Itens.First(i => i.Sequencial == sequencial);
                Itens.Remove(itemARemover);
                return true;
            }
            return false;
        }

        public bool AtualizarItemPorSequencial(int sequencial, Produto produto, int quantidade, string observacao){
            if(Itens.Any(i => i.Sequencial == sequencial)){
                var itemDaLista = Itens.First(i => i.Sequencial == sequencial);
                itemDaLista.AtualizarDadosItem(produto,quantidade,observacao);
                return true;
            }
            return false;
        }

        public void FecharPedidoParaPagamento(){
            Status = StatusPedido.AguardandoPagamento;
        }

        private decimal CalcularValorTotal(){
            var valorItens = Itens.Sum(i => i.Valor);
            //TODO: Adição de taxa/imposto ?
            return valorItens;
        }

        private int ProximoSequencial()
        {
            if(Itens.Count == 0)
            {
                return 1; 
            }
            int ultimoSequencial = Itens.Max(i => i.Sequencial);
            return ultimoSequencial + 1;
        }
    }
}