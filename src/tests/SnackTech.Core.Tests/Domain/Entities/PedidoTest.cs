using System;
using System.Collections.Generic;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using Xunit;

namespace SnackTech.Core.Tests.Domain.Entities
{
    public class PedidoTest
    {
        [Fact]
        public void Pedido_Construtor_NaoAceitaIdNulo()
        {
            Assert.Throws<ArgumentException>(() => new Pedido(null, new DataPedidoValida(DateTime.Now), new StatusPedidoValido(1), new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191")));
        }

        [Fact]
        public void Pedido_Construtor_NaoAceitaDataCriacaoInvalida()
        {
            Assert.Throws<ArgumentException>(() => new Pedido(Guid.NewGuid(), DateTime.MinValue, new StatusPedidoValido(1), new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191")));
            Assert.Throws<ArgumentException>(() => new Pedido(Guid.NewGuid(), DateTime.Now.AddDays(1), new StatusPedidoValido(1), new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191")));
        }

        [Fact]
        public void Pedido_Construtor_NaoAceitaStatusInvalido()
        {
            Assert.Throws<ArgumentException>(() => new Pedido(Guid.NewGuid(), new DataPedidoValida(DateTime.Now), 99, new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191")));
            Assert.Throws<ArgumentException>(() => new Pedido(Guid.NewGuid(), new DataPedidoValida(DateTime.Now), 0, new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191")));
        }

        [Fact]
        public void Pedido_Construtor_NaoAceitaClienteNulo()
        {
            Assert.Throws<ArgumentException>(() => new Pedido(Guid.NewGuid(), new DataPedidoValida(DateTime.Now), new StatusPedidoValido(1), null));
        }

        [Fact]
        public void Pedido_Construtor_CriaObjetoComValoresCorretos()
        {
            var id = Guid.NewGuid();
            var dataCriacao = new DataPedidoValida(DateTime.Now);
            var status = new StatusPedidoValido(1);
            var cliente = new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191");

            var pedido = new Pedido(id, dataCriacao, status, cliente);

            Assert.Equal(id, pedido.Id.Valor);
            Assert.Equal(dataCriacao, pedido.DataCriacao);
            Assert.Equal(status, pedido.Status);
            Assert.Equal(cliente, pedido.Cliente);
        }

        [Fact]
        public void Pedido_CriarPedidoComItens_Corretamente()
        {
            var id = Guid.NewGuid();
            var dataCriacao = new DataPedidoValida(DateTime.Now);
            var status = new StatusPedidoValido(1);
            var cliente = new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191");
            var itens = new List<PedidoItem> { new PedidoItem(Guid.NewGuid(), new Produto(Guid.NewGuid(), 1, "nome", "", 10.99m), 1, "observacao") };

            var pedido = new Pedido(id, dataCriacao, status, cliente, itens);

            Assert.Equal(itens, pedido.Itens);
        }

        [Fact]
        public void Pedido_FecharPedidoParaPagamento_NaoAceitaItensVazios()
        {
            var pedido = new Pedido(Guid.NewGuid(), new DataPedidoValida(DateTime.Now), new StatusPedidoValido(1), new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191"));

            Assert.Throws<Exception>(() => pedido.FecharPedidoParaPagamento());
        }

        [Fact]
        public void Pedido_FecharPedidoParaPagamento_AlteraStatusCorretamente()
        {
            var id = Guid.NewGuid();
            var dataCriacao = new DataPedidoValida(DateTime.Now);
            var status = new StatusPedidoValido(1);
            var cliente = new Cliente(Guid.NewGuid(), "nome", "email@email.com", "00000000191");
            var itens = new List<PedidoItem> { new PedidoItem(Guid.NewGuid(), new Produto(Guid.NewGuid(), 1, "nome", "", 10.99m), 1, "observacao") };
            var pedido = new Pedido(id, dataCriacao, status, cliente, itens);

            pedido.FecharPedidoParaPagamento();

            Assert.Equal(StatusPedidoValido.AguardandoPagamento, pedido.Status);
        }
    }
}