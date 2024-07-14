using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;

namespace SnackTech.Domain.Tests.ModelsTests
{
    public class PedidoTests
    {
        [Fact]
        public void CreatePedidoWithClientNull(){
            try{
                Cliente clienteDoPedido = null;
                var pedido = new Pedido(clienteDoPedido);
                Assert.Fail($"O pedido {pedido.Id} foi criado com cliente nulo.");
            }
            catch(ArgumentException ex){
                Assert.Equal("cliente não pode ser nulo. (Parameter 'cliente')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Pedido. {ex.Message}");
            }
        }

        [Fact]
        public void CreatePedidoWithClient(){
            try{
                CriarPedidoSoComCliente();
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Pedido. {ex.Message}");
            }
        }

        [Fact]
        public void CreatePedidoWithItens(){
            try{
                CriarPedidoComItens();
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Pedido. {ex.Message}");
            }
        }

        [Fact]
        public void CreatePedidoTestValor(){
            try{
                var pedidoComItens = CriarPedidoComItens();
                var calculoEsperado = 30*2;
                Assert.Equal(calculoEsperado,pedidoComItens.Valor);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Pedido. {ex.Message}");
            }
        }

        [Fact]
        public void AdicionarItemProductNull(){
            try{
                var pedido = CriarPedidoSoComCliente();
                Produto novoProduto = null;
                pedido.AdicionarItem(novoProduto,1,"");
                Assert.Fail("Foi possível adicionar um produto nulo a um pedido");
            }
            catch(ArgumentException ex){
                Assert.Equal("produto não pode ser nulo. (Parameter 'produto')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu uma exception inesperada. {ex.Message}");
            }
        }

        [Fact]
        public void AdicionarItemQuantityLessThanOne(){
            try{
                var pedido = CriarPedidoSoComCliente();
                pedido.AdicionarItem(CriarProduto(CategoriaProduto.Lanche,"lanche", 20),0,"");
                Assert.Fail("Foi possível adicionar um produto com quantidade igual ou menor a zero");
            }
            catch(ArgumentException ex){
                Assert.Equal("quantidade precisa ser maior do que zero. (Parameter 'quantidade')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu uma exception inesperada. {ex.Message}");
            }
        }

        [Fact]
        public void AdicionarItemWithSuccess(){
            try{
                var pedido = CriarPedidoSoComCliente();
                pedido.AdicionarItem(CriarProduto(CategoriaProduto.Lanche,"lanche", 20),2,"");
                Assert.Equal(1,pedido.Itens.Count());

                var pedidoItem = pedido.Itens[0];
                Assert.NotNull(pedidoItem);
                Assert.Equal(1,pedidoItem.Sequencial);
                Assert.Equal(2,pedidoItem.Quantidade);

                var produto = pedidoItem.Produto;
                Assert.NotNull(produto);
                Assert.Equal(CategoriaProduto.Lanche,produto.Categoria);
                Assert.Equal("lanche",produto.Nome);
                Assert.Equal(20,produto.Valor);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu uma exception inesperada. {ex.Message}");
            }
        }

        [Fact]
        public void RemoveItemByIdWithSuccess(){
            var pedido = CriarPedidoComItens();
            var itemSequencial = pedido.Itens[0].Sequencial;
            var result = pedido.RemoverItemPorSequencial(itemSequencial);
            Assert.True(result);
            Assert.False(pedido.Itens.Any());
        }

        [Fact]
        public void RemoveItemByIdWithoutSuccess(){
            var pedido = CriarPedidoComItens();
            var itemSequencial = 4;
            var result = pedido.RemoverItemPorSequencial(itemSequencial);
            Assert.False(result);
            Assert.True(pedido.Itens.Any());
        }

        [Fact]
        public void UpdateItemWithSuccess(){
            var pedido = CriarPedidoComItens();
            var itemSequencial = pedido.Itens[0].Sequencial;
            var novoProduto = CriarProduto(CategoriaProduto.Acompanhamento,"Batata Frita",15);
            var novaObservacao = "Breve observação";
            var result = pedido.AtualizarItemPorSequencial(itemSequencial,novoProduto,3,novaObservacao);
            Assert.True(result);

            var pedidoItem = pedido.Itens[0];
            Assert.NotNull(pedidoItem);
            Assert.Equal(1,pedidoItem.Sequencial);
            Assert.Equal(3,pedidoItem.Quantidade);
            Assert.Equal(novaObservacao,pedidoItem.Observacao);

            var produto = pedidoItem.Produto;
            Assert.NotNull(produto);
            Assert.Equal(CategoriaProduto.Acompanhamento,produto.Categoria);
            Assert.Equal("Batata Frita",produto.Nome);
            Assert.Equal(15,produto.Valor);
        }

        [Fact]
        public void UpdateItemWithoutSuccess(){
            var pedido = CriarPedidoComItens();
            var itemSequencial = 4;
            var novoProduto = CriarProduto(CategoriaProduto.Acompanhamento,"Batata Frita",15);
            var novaObservacao = "Breve observação";
            var result = pedido.AtualizarItemPorSequencial(itemSequencial,novoProduto,3,novaObservacao);
            Assert.False(result);

            var pedidoItem = pedido.Itens[0];
            Assert.NotNull(pedidoItem);
            Assert.Equal(1,pedidoItem.Sequencial);
            Assert.Equal(2,pedidoItem.Quantidade);
            Assert.Equal("",pedidoItem.Observacao);

            var produto = pedidoItem.Produto;
            Assert.NotNull(produto);
            Assert.Equal(CategoriaProduto.Lanche,produto.Categoria);
            Assert.Equal("Lanche",produto.Nome);
            Assert.Equal(30,produto.Valor);
        }

        [Fact]
        public void FecharPedidoParaPagamentoWithSuccess(){
            var pedido = CriarPedidoComItens();
            pedido.FecharPedidoParaPagamento();
            Assert.Equal(StatusPedido.AguardandoPagamento,pedido.Status);
        }

        private Pedido CriarPedidoComItens()
            => new Pedido(CriarCliente(),CriarListaPedidoItem());

        private Pedido CriarPedidoSoComCliente() 
            => new Pedido(CriarCliente());

        private Cliente CriarCliente()
            => new Cliente("Nome Cliente","email@gmail.com","89934782014");

        private IList<PedidoItem> CriarListaPedidoItem(){
            var lista = new List<PedidoItem>(){
                new PedidoItem(1, new Produto(CategoriaProduto.Lanche,"Lanche","descrição",30.0M),2,"")
            };

            return lista;
        }

        private Produto CriarProduto(CategoriaProduto categoria,string nome, decimal valor)
            => new Produto(categoria,nome,nome,valor);
    }
}