using SnackTech.Domain.Enums;
using SnackTech.Domain.Entities;

namespace SnackTech.Domain.Tests.ModelsTests
{
    public class ProdutoTests
    {
        [Fact]
        public void CreateProductWithTituloNull()
        {
            try{
                CriarProduto(CategoriaProduto.Acompanhamento,null!,"descricao",10);
                Assert.Fail("Produto não pode ter o nome nulo");
            }
            catch(ArgumentException ex){
                Assert.Equal("nome não pode ser nulo, vazio ou texto em branco. (Parameter 'nome')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Produto. {ex.Message}");
            }
        }

        [Fact]
        public void CreateProductWithTituloEmpty()
        {
            try{
                CriarProduto(CategoriaProduto.Acompanhamento,"","descricao",10);
                Assert.Fail("Produto não pode ter o nome vazio");
            }
            catch(ArgumentException ex){
                Assert.Equal("nome não pode ser nulo, vazio ou texto em branco. (Parameter 'nome')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Produto. {ex.Message}");
            }
        }

        [Fact]
        public void CreateProductWithTituloWithWhiteSpaceOnly()
        {
            try{
                CriarProduto(CategoriaProduto.Acompanhamento,"  ","descricao",10);
                Assert.Fail("Produto não pode ter o nome somente com espaço em branco");
            }
            catch(ArgumentException ex){
                Assert.Equal("nome não pode ser nulo, vazio ou texto em branco. (Parameter 'nome')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Produto. {ex.Message}");
            }
        }

        [Fact]
        public void CreateProductWithDescriptionNull()
        {
            try{
                CriarProduto(CategoriaProduto.Acompanhamento,"nome",null!,10);
                Assert.Fail("Produto não pode ter a descrição nula");
            }
            catch(ArgumentException ex){
                Assert.Equal("descricao não pode ser nulo ou vazio. (Parameter 'descricao')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Produto. {ex.Message}");
            }
        }

        [Fact]
        public void CreateProductWithDescriptionEmpty()
        {
            try{
                CriarProduto(CategoriaProduto.Acompanhamento,"nome","",10);
                Assert.Fail("Produto não pode ter a descrição vazia");
            }
            catch(ArgumentException ex){
                Assert.Equal("descricao não pode ser nulo ou vazio. (Parameter 'descricao')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Produto. {ex.Message}");
            }
        }

        [Fact]
        public void CreateProductWithValueLessThanZero()
        {
            try{
                CriarProduto(CategoriaProduto.Acompanhamento,"nome","descricao",-10);
                Assert.Fail("Produto não pode ter o valor menor que zero");
            }
            catch(ArgumentException ex){
                Assert.Equal("valor precisa ser maior do que zero. (Parameter 'valor')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Produto. {ex.Message}");
            }
        }

        [Fact]
        public void CreateProductWithValueEqualToZero()
        {
            try{
                CriarProduto(CategoriaProduto.Acompanhamento,"nome","descricao",0);
                Assert.Fail("Produto não pode ter o valor igual a zero");
            }
            catch(ArgumentException ex){
                Assert.Equal("valor precisa ser maior do que zero. (Parameter 'valor')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Produto. {ex.Message}");
            }
        }

        [Fact]
        public void CreateProductWithSuccess()
        {
            try{
                var newProduto = CriarProduto(CategoriaProduto.Acompanhamento,"nome","descricao",10);
                Assert.Equal(CategoriaProduto.Acompanhamento, newProduto.Categoria);
                Assert.Equal("nome", newProduto.Nome);
                Assert.Equal("descricao", newProduto.Descricao);
                Assert.Equal(10, newProduto.Valor);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Produto. {ex.Message}");
            }
        }

        [Fact]
        public void UpdateProductWithSuccess(){
            try{
                var produto = CriarProduto(CategoriaProduto.Lanche,"X-Salada","Lanche com tomate",20);

                produto.AtualizarDadosProduto(CategoriaProduto.Acompanhamento,"Nuggets","Frango empanado",8);

                Assert.NotNull(produto);
                Assert.Equal(CategoriaProduto.Acompanhamento,produto.Categoria);
                Assert.Equal("Nuggets",produto.Nome);
                Assert.Equal("Frango empanado",produto.Descricao);
                Assert.Equal(8,produto.Valor);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado. {ex.Message}");
            }
        }

        private static Produto CriarProduto(CategoriaProduto categoria, string nome, string descricao, decimal valor){
            var newProduto = new Produto(categoria,nome,descricao,valor);
            return newProduto;
        }
    }
}