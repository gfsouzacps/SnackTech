using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Tests.GuardsTests
{
    public class EmailGuardTests
    {
        [Fact]
        public void AgainstInvalidEmailCaseNull()
        {
            try{
                string email = null;
                EmailGuard.AgainstInvalidEmail(email, nameof(email));
                Assert.Fail("Guarda precisa lançar exception caso email seja nulo");
            }
            catch(ArgumentException ex){
                Assert.Equal("email não pode ser um e-mail nulo ou somente espaços em branco. (Parameter 'email')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidEmailCaseWhiteSpace()
        {
            try{
                string email = "    ";
                EmailGuard.AgainstInvalidEmail(email, nameof(email));
                Assert.Fail("Guarda precisa lançar exception caso email seja nulo");
            }
            catch(ArgumentException ex){
                Assert.Equal("email não pode ser um e-mail nulo ou somente espaços em branco. (Parameter 'email')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidEmailCaseEmpty()
        {
            try{
                string email = "";
                EmailGuard.AgainstInvalidEmail(email, nameof(email));
                Assert.Fail("Guarda precisa lançar exception caso email esteja vazio");
            }
            catch(ArgumentException ex){
                Assert.Equal("email não pode ser um e-mail nulo ou somente espaços em branco. (Parameter 'email')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidEmailCaseWithoutAt(){
            try{
                string email = "emailsemarroba.com";
                EmailGuard.AgainstInvalidEmail(email, nameof(email));
                Assert.Fail("Guarda precisa lançar exception caso email não tenha arroba");
            }
            catch(ArgumentException ex){
                Assert.Equal("email tem o valor emailsemarroba.com que não é um e-mail válido. (Parameter 'email')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidEmailCaseWithoutDomain(){
            try{
                string email = "emailsemdominiopontocom";
                EmailGuard.AgainstInvalidEmail(email, nameof(email));
                Assert.Fail("Guarda precisa lançar exception caso email não tenha domínio");
            }
            catch(ArgumentException ex){
                Assert.Equal("email tem o valor emailsemdominiopontocom que não é um e-mail válido. (Parameter 'email')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar uma exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstInvalidEmailCaseWithValidEmail(){
            try{
                string email = "emailvalido@gmail.com";
                EmailGuard.AgainstInvalidEmail(email, nameof(email));
            }
            catch(Exception ex){
                Assert.Fail($"Guarda lançou uma exception com um e-mail válido. {ex.Message}");
            }
        }
    }
}