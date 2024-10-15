using System;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using Xunit;

namespace SnackTech.Core.Tests.Domain.Entities
{
    public class ProdutoTest
    {
        [Fact]
        public void Produto_Construtor_NaoAceitaIdNulo()
        {
            Assert.Throws<ArgumentException>(() => new Produto(null, CategoriaProdutoValido.Acompanhamento, "nome", "descricao", 10.99m));
        }

        [Fact]
        public void Produto_Construtor_NaoAceitaVazio()
        {
            Assert.Throws<ArgumentException>(() => new Produto(Guid.NewGuid(), CategoriaProdutoValido.Acompanhamento, "", "descricao", 10.99m));
            Assert.Throws<ArgumentException>(() => new Produto(Guid.NewGuid(), CategoriaProdutoValido.Acompanhamento, "   ", "descricao", 10.99m));
        }

        [Fact]
        public void Produto_Construtor_NaoAceitaValoMenorOuIgualAZero()
        {
            Assert.Throws<ArgumentException>(() => new Produto(Guid.NewGuid(), CategoriaProdutoValido.Acompanhamento, "nome", "descricao", -1));
        }

        [Fact]
        public void Produto_Construtor_CriaObjetoComValoresCorretos()
        {
            var id = Guid.NewGuid();
            var categoria = CategoriaProdutoValido.Acompanhamento;
            var nome = "nome";
            var descricao = "descricao";
            var valor = 10.99m;

            var produto = new Produto(id, categoria, nome, descricao, valor);

            Assert.Equal(id, produto.Id.Valor);
            Assert.Equal(categoria, produto.Categoria);
            Assert.Equal(nome, produto.Nome.Valor);
            Assert.Equal(descricao, produto.Descricao);
            Assert.Equal(valor, produto.Valor.Valor);
        }

        [Fact]
        public void Produto_Atualizar_NaoAceitaNomeVazio()
        {
            var produto = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Acompanhamento, "nome", "descricao", 10.99m);

            Assert.Throws<ArgumentException>(() => produto.Atualizar(CategoriaProdutoValido.Acompanhamento, "", "descricao", 10.99m));
            Assert.Throws<ArgumentException>(() => produto.Atualizar(CategoriaProdutoValido.Acompanhamento, "   ", "descricao", 10.99m));
        }

        [Fact]
        public void Produto_Atualizar_NaoAceitaValorMenorOuIgualAZero()
        {
            var produto = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Acompanhamento, "nome", "descricao", 10.99m);

            Assert.Throws<ArgumentException>(() => produto.Atualizar(CategoriaProdutoValido.Acompanhamento, "nome", "descricao", -1));
        }

        [Fact]
        public void Produto_Atualizar_AtualizaValoresCorretamente()
        {
            var produto = new Produto(Guid.NewGuid(), CategoriaProdutoValido.Acompanhamento, "nome", "descricao", 10.99m);
            var novaCategoria = CategoriaProdutoValido.Sobremesa;
            var novoNome = "novo nome";
            var novaDescricao = "nova descricao";
            var novoValor = 9.99m;

            produto.Atualizar(novaCategoria, novoNome, novaDescricao, novoValor);

            Assert.Equal(novaCategoria, produto.Categoria);
            Assert.Equal(novoNome, produto.Nome);
            Assert.Equal(novaDescricao, produto.Descricao);
            Assert.Equal(novoValor, produto.Valor.Valor);
        }
    }
}