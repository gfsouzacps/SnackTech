using SnackTech.Domain.Enums;
using SnackTech.Domain.Models;

namespace SnackTech.Domain.Tests.ModelsTests
{
    public class ProdutoTests
    {
        [Fact]
        public void CreateProductWithTituloNull()
        {
            try{
                CriarProduto(CategoriaProduto.Acompanhamento,null,"descricao",10);
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
                CriarProduto(CategoriaProduto.Acompanhamento,"nome",null,10);
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

        private Produto CriarProduto(CategoriaProduto categoria, string nome, string descricao, decimal valor){
            var newProduto = new Produto(categoria,nome,descricao,valor);
            return newProduto;
        }
    }
}