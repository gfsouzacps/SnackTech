using System;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using Xunit;

namespace SnackTech.Core.Tests.Domain.Entities
{
    public class PedidoItemTest
    {
        private Produto _produtoExemplo = new Produto(Guid.NewGuid(), 1, "nome", "", 10.99m);

        [Fact]
        public void PedidoItem_Construtor_NaoAceitaIdNulo()
        {
            Assert.Throws<ArgumentException>(() => new PedidoItem(null, _produtoExemplo, 2, ""));
        }

        [Fact]
        public void PedidoItem_Construtor_NaoAceitaProdutoNulo()
        {
            Assert.Throws<ArgumentException>(() => new PedidoItem(Guid.NewGuid(), null, 2, ""));
        }

        [Fact]
        public void PedidoItem_Construtor_NaoAceitaQuantidadeNulaOuMenorOuIgualAZero()
        {
            Assert.Throws<ArgumentException>(() => new PedidoItem(Guid.NewGuid(), _produtoExemplo, 0, ""));
            Assert.Throws<ArgumentException>(() => new PedidoItem(Guid.NewGuid(), _produtoExemplo, -1, ""));
        }

        [Fact]
        public void PedidoItem_Construtor_CriaObjetoComValoresCorretos()
        {
            var id = Guid.NewGuid();
            var quantidade = 2;

            var pedidoItem = new PedidoItem(id, _produtoExemplo, quantidade, "");

            Assert.Equal(id, pedidoItem.Id.Valor);
            Assert.Equal(_produtoExemplo, pedidoItem.Produto);
            Assert.Equal(quantidade, pedidoItem.Quantidade.Valor);
        }

        [Fact]
        public void PedidoItem_Atualizar_NaoAceitaQuantidadeNulaOuMenorOuIgualAZero()
        {
            var pedidoItem = new PedidoItem(Guid.NewGuid(), _produtoExemplo, 2, "");

            Assert.Throws<ArgumentException>(() => pedidoItem.Atualizar(0, pedidoItem.Produto, "observacao"));
            Assert.Throws<ArgumentException>(() => pedidoItem.Atualizar(-1, pedidoItem.Produto, "observacao"));
        }

        [Fact]
        public void PedidoItem_Atualizar_NaoAceitaProdutoNulo()
        {
            var pedidoItem = new PedidoItem(Guid.NewGuid(), _produtoExemplo, 2, "");

            Assert.Throws<ArgumentException>(() => pedidoItem.Atualizar(3, null, "observacao"));
        }

        [Fact]
        public void PedidoItem_Atualizar_AtualizaValoresCorretamente()
        {
            var pedidoItem = new PedidoItem(Guid.NewGuid(), _produtoExemplo, 2, "");
            var novaQuantidade = 3;
            var novoProduto = new Produto(Guid.NewGuid(), 1, "novo nome", "" , 9.99m);
            var novaObservacao = "nova observacao";

            pedidoItem.Atualizar(novaQuantidade, novoProduto, novaObservacao);

            Assert.Equal(novaQuantidade, pedidoItem.Quantidade.Valor);
            Assert.Equal(novoProduto, pedidoItem.Produto);
            Assert.Equal(novaObservacao, pedidoItem.Observacao);
        }

        [Fact]
        public void PedidoItem_Valor_RetornaValorCorreto()
        {
            var pedidoItem = new PedidoItem(Guid.NewGuid(), _produtoExemplo, 2, "");

            var valor = pedidoItem.Valor();

            Assert.Equal(2 * _produtoExemplo.Valor.Valor, valor.Valor);
        }
    }
}