using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Tests.GuardsTests
{
    public class CpfGuardTests
    {
        [Fact]
        public void AgainstInvalidCpfCaseNull()
        {
            try{
                string? cpf = null;
                CpfGuard.AgainstInvalidCpf(cpf!, nameof(cpf));
                Assert.Fail("Guarda precisa lançar exception caso cpf seja nulo");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf não pode ser nulo ou espaços em branco. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidCpfCaseWhiteSpace()
        {
            try{
                string cpf = "    ";
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
                Assert.Fail("Guarda precisa lançar exception caso cpf seja uma string somente com espaços em branco");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf não pode ser nulo ou espaços em branco. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidCpfCaseEmpty()
        {
            try{
                string cpf = "";
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
                Assert.Fail("Guarda precisa lançar exception caso cpf seja uma string vazia");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf não pode ser nulo ou espaços em branco. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidCpfCaseInvalidCpfWithLetters(){
            try{
                string cpf = "A2369A7A8O7";
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
                Assert.Fail("Guarda precisa lançar exception caso cpf tenha letras");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf gerou erro ao ser validado como CPF. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidCpfCaseInvalidCpfWithFormattingAndWithLetters(){
            try{
                string cpf = "A23.69A.7A8-O7";
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
                Assert.Fail("Guarda precisa lançar exception caso cpf tenha letras e formatado");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf gerou erro ao ser validado como CPF. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception ex){
                Assert.Fail($"Guarda precisa lançar uma exception do tipo ArgumentException. {ex.Message}");
            }
        }

        [Fact]
        public void AgainstInvalidCpfCaseInvalidCpfNumber(){
            try{
                string cpf = "12345678901";
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
                Assert.Fail("Guarda precisa lançar exception caso cpf tenha números inválidos");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf com valor 12345678901 não é um CPF válido. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidCpfCaseRepetitiveNumbers(){
            try{
                string cpf = "33333333333";
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
                Assert.Fail("Guarda precisa lançar exception caso cpf tenha números repetidos");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf com valor 33333333333 não é um CPF válido. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidCpfCaseRepetitiveNumbersWithFormat(){
            try{
                string cpf = "333.333.333-33";
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
                Assert.Fail("Guarda precisa lançar exception caso cpf tenha números repetidos formatados");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf com valor 333.333.333-33 não é um CPF válido. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidCpfCaseValidCpfFormatted(){
            try{
                string cpf = "580.914.540-07";
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
            }
            catch(Exception ex){
                Assert.Fail($"Guarda lançou uma exception com um valor de CPF válido. {ex.Message}");
            }
        }

        [Fact]
        public void AgainstInvalidCpfCaseValidCpf(){
            try{
                string cpf = "58091454007";
                CpfGuard.AgainstInvalidCpf(cpf, nameof(cpf));
            }
            catch(Exception ex){
                Assert.Fail($"Guarda lançou uma exception com um valor de CPF válido. {ex.Message}");
            }
        }

        [Fact]
        public void AgainstInvalidCpfLengthLessThan11(){
            try{
                string cpf = "1234567890";
                CpfGuard.AgainstInvalidCpf(cpf,nameof(cpf));
                Assert.Fail("Guarda precisa lançar exception caso cpf tenha números repetidos formatados");
            }
            catch(ArgumentException ex){
                Assert.Equal("cpf com valor 1234567890 não é um CPF válido. (Parameter 'cpf')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }
    }
}