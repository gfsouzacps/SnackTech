using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;

namespace SnackTech.Domain.Tests.ModelsTests
{
    public class PedidoItemTests
    {
        [Fact]
        public void CreatePedidoItemWithObservation()
        {
            var produto = CriarProduto(CategoriaProduto.Bebida, "Refrigerante", 5);
            var pedidoItem = CriarPedidoItem(produto,1,"observação");
            Assert.Equal("observação",pedidoItem.Observacao);
        }

        [Fact]
        public void CreatePedidoItemWithNullObservation(){
            var produto = CriarProduto(CategoriaProduto.Bebida, "Refrigerante", 5);
            var pedidoItem = CriarPedidoItem(produto,1,null!);
            Assert.Equal(string.Empty,pedidoItem.Observacao);
        }

        [Fact]
        public void AtualizarDadosItemWithNullProduct(){
            try{
                var produto = CriarProduto(CategoriaProduto.Bebida, "Refrigerante", 5);
                var pedidoItem = CriarPedidoItem(produto,1,"observação");

                pedidoItem.AtualizarDadosItem(null!,1,"sabor morango");

                Assert.Fail("Permitiu atualizar dados do pedido item com produto nulo");
            }
            catch(ArgumentException ex){
                Assert.Equal("produto não pode ser nulo. (Parameter 'produto')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Lançou uma exception inesperada. {ex.Message}");
            }
        }

        [Fact]
        public void AtualizarDadosItemWithQuantityLessThanOne(){
            try{
                var produto = CriarProduto(CategoriaProduto.Bebida, "Refrigerante", 5);
                var pedidoItem = CriarPedidoItem(produto,1,"observação");

                var novoProduto = CriarProduto(CategoriaProduto.Sobremesa,"Sorvete",6);
                pedidoItem.AtualizarDadosItem(novoProduto,0,"sabor morango");

                Assert.Fail("Permitiu atualizar dados do pedido item com quantidade menor que 1");
            }
            catch(ArgumentException ex){
                Assert.Equal("quantidade precisa ser maior do que zero. (Parameter 'quantidade')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Lançou uma exception inesperada. {ex.Message}");
            }
        }

        [Fact]
        public void AtualizarDadosItemWithSuccess(){
            try{
                var produto = CriarProduto(CategoriaProduto.Bebida, "Refrigerante", 5);
                var pedidoItem = CriarPedidoItem(produto,1,"observação");

                var novoProduto = CriarProduto(CategoriaProduto.Sobremesa,"Sorvete",6);
                pedidoItem.AtualizarDadosItem(novoProduto,1,"sabor morango");

                Assert.NotNull(pedidoItem.Produto);
                Assert.Equal(1,pedidoItem.Quantidade);
                Assert.Equal(1,pedidoItem.Sequencial);
                Assert.Equal("sabor morango",pedidoItem.Observacao);

                var pedidoItemProduto = pedidoItem.Produto;
                Assert.NotNull(pedidoItemProduto);
                Assert.Equal("Sorvete",pedidoItemProduto.Nome);
                Assert.Equal(CategoriaProduto.Sobremesa,pedidoItemProduto.Categoria);
                Assert.Equal(6,pedidoItemProduto.Valor);
            }
            catch(Exception ex){
                Assert.Fail($"Lançou uma exception inesperada. {ex.Message}");
            }
        }

        private static PedidoItem CriarPedidoItem(Produto produto, int quantidade, string observacao)
            => new(1,produto,quantidade,observacao);

        private static Produto CriarProduto(CategoriaProduto categoria,string nome, decimal valor)
            => new(categoria,nome,nome,valor);
    }
}