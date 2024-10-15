using System;
using SnackTech.Core.Domain.Entities;
using SnackTech.Core.Domain.Types;
using Xunit;

namespace SnackTech.Core.Tests.Domain.Entities
{
    public class ClienteTest
    {
        [Fact]
        public void Cliente_Construtor_NaoAceitaIdNulo()
        {
            Assert.Throws<ArgumentException>(() => new Cliente(null, "nome", "email", "cpf"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Cliente_Construtor_NaoAceitaNomeNuloOuVazio(string? nome)
        {
            Assert.Throws<ArgumentException>(() => new Cliente(Guid.NewGuid(), nome, "email@email.com", "00000000191"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("invalid-email")]
        public void Cliente_Construtor_NaoAceitaEmailInvalido(string? email)
        {
            Assert.Throws<ArgumentException>(() => new Cliente(Guid.NewGuid(), "nome", email, "00000000191"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("0000000000")]
        [InlineData("invalid-cpf")]
        public void Cliente_Construtor_NaoAceitaCpfInvalido(string? cpf)
        {
            Assert.Throws<ArgumentException>(() => new Cliente(Guid.NewGuid(), "nome", "email@email.com", cpf));
        }

        [Fact]
        public void Cliente_Construtor_CriaObjetoComValoresCorretos()
        {
            var id = Guid.NewGuid();
            var nome = "nome";
            var email = "email@email.com";
            var cpf = "00000000191";

            var cliente = new Cliente(id, nome, email, cpf);

            Assert.Equal(id, cliente.Id.Valor);
            Assert.Equal(nome, cliente.Nome.Valor);
            Assert.Equal(email, cliente.Email.Valor);
            Assert.Equal(cpf, cliente.Cpf.Valor);
        }
    }
}