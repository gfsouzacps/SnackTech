using SnackTech.Domain.Models;

namespace SnackTech.Domain.Tests.ModelsTests
{
    public class ClienteTests
    {
        [Fact]
        public void CreateClienteWithNomeNull()
        {
            try{
                CriarCliente(null!,"email","12345678901");
                Assert.Fail("Cliente não pode ter o nome nulo");
            }
            catch(ArgumentException ex){
                Assert.Equal("nome não pode ser nulo, vazio ou texto em branco. (Parameter 'nome')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }

        [Fact]
        public void CreateClienteWithNomeEmpty()
        {
            try{
                CriarCliente("","email","12345678901");
                Assert.Fail("Cliente não pode ter o nome vazio");
            }
            catch(ArgumentException ex){
                Assert.Equal("nome não pode ser nulo, vazio ou texto em branco. (Parameter 'nome')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }

        [Fact]
        public void CreateClienteWithNomeWithWhiteSpaceOnly()
        {
            try{
                CriarCliente("   ","email","12345678901");
                Assert.Fail("Cliente não pode ter o nome com espaço em branco");
            }
            catch(ArgumentException ex){
                Assert.Equal("nome não pode ser nulo, vazio ou texto em branco. (Parameter 'nome')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }

        [Fact]
        public void CreateClienteWithEmailNull()
        {
            try{
                CriarCliente("nome",null!,"12345678901");
                Assert.Fail("Cliente não pode ter o email nulo");
            }
            catch(ArgumentException ex){
                Assert.Equal("email não pode ser um e-mail nulo ou somente espaços em branco. (Parameter 'email')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }

        [Fact]
        public void CreateClienteWithEmailEmpty()
        {
            try{
                CriarCliente("nome","","12345678901");
                Assert.Fail("Cliente não pode ter o email vazio");
            }
            catch(ArgumentException ex){
                Assert.Equal("email não pode ser um e-mail nulo ou somente espaços em branco. (Parameter 'email')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }

        [Fact]
        public void CreateClienteWithEmailWithWhiteSpaceOnly()
        {
            try{
                CriarCliente("nome","    ","12345678901");
                Assert.Fail("Cliente não pode ter o email somente com espaços em branco");
            }
            catch(ArgumentException ex){
                Assert.Equal("email não pode ser um e-mail nulo ou somente espaços em branco. (Parameter 'email')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }

        [Fact]
        public void CreateClienteWithEmailInvalid()
        {
            try{
                CriarCliente("nome","email","12345678901");
                Assert.Fail("Cliente não pode ter o email com valor inválido");
            }
            catch(ArgumentException ex){
                Assert.Equal("email tem o valor email que não é um e-mail válido. (Parameter 'email')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }

        [Fact]
        public void CreateClienteWithCpfNull()
        {
            try{
                CriarCliente("nome","email@gmail.com",null!);
                Assert.Fail("Cliente não pode ter o CPF nulo");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf não pode ser nulo ou espaços em branco. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }

        [Fact]
        public void CreateClienteWithCpfEmpty()
        {
            try{
                CriarCliente("nome","email@hotmail.com.br","");
                Assert.Fail("Cliente não pode ter o CPF vazio");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf não pode ser nulo ou espaços em branco. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }
        
        [Fact]
        public void CreateClienteWithCpfWithWhiteSpaceOnly()
        {
            try{
                CriarCliente("nome","email@gmail.com","    ");
                Assert.Fail("Cliente não pode ter o CPF somente com espaços em branco");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf não pode ser nulo ou espaços em branco. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }

        [Fact]
        public void CreateClienteWithInvalidCpf()
        {
            try{
                CriarCliente("nome","email@bol.com.br","12345678901");
                Assert.Fail("Cliente não pode ter o CPF somente com espaços em branco");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf com valor 12345678901 não é um CPF válido. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }

        [Fact]
        public void CreateClienteWithSuccess()
        {
            try{
                var cliente = CriarCliente("nome","email@outlook.com","62332646000");
                Assert.Equal("nome",cliente.Pessoa.Nome);
                Assert.Equal("email@outlook.com",cliente.Email);
                Assert.Equal("62332646000",cliente.CPF);
            }
            catch(Exception ex){
                Assert.Fail($"Ocorreu erro inesperado ao instanciar Cliente. {ex.Message}");
            }
        }


        private static Cliente CriarCliente(string nome, string email, string cpf)
            => new(nome,email,cpf);
    }
}