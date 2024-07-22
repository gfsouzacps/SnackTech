using SnackTech.Domain.Guards;

namespace SnackTech.Domain.Tests.GuardsTests
{
    public class CustomGuardsTests
    {
        [Fact]
        public void AgainstStringNullOrEmptyCaseStringNull()
        {
            try{
                string? newText = null;
                CustomGuards.AgainstStringNullOrEmpty(newText!, nameof(newText));
                Assert.Fail("Guarda precisa lançar exception caso string esteja nula");
            }   
            catch(ArgumentException ex){
                Assert.Equal("newText não pode ser nulo ou vazio. (Parameter 'newText')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar exception do tipo ArgumentException");
            }         
        }

        [Fact]
        public void AgainstStringNullOrEmptyCaseStringEmpty()
        {
            try{
                string newText = string.Empty;
                CustomGuards.AgainstStringNullOrEmpty(newText, nameof(newText));
                Assert.Fail("Guarda precisa lançar exception caso string esteja vazia.");
            }
            catch(ArgumentException ex){
                Assert.Equal("newText não pode ser nulo ou vazio. (Parameter 'newText')", ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstStringNullOrEmptyCaseStringWithValue()
        {
            try{
                string newText = "string com valor";
                CustomGuards.AgainstStringNullOrEmpty(newText, nameof(newText));
            }
            catch(Exception ex){
                Assert.Fail($"Guarda lançou exception para uma string preenchida. {ex.Message}");
            }
        }

        [Fact]
        public void AgainstStringNullOrWhiteSpaceCaseNull(){
            try{
                string? newText = null;
                CustomGuards.AgainstStringNullOrWhiteSpace(newText!, nameof(newText));
                Assert.Fail("Guarda precisa lançar exception caso string esteja nula");
            }
            catch(ArgumentException ex){
                 Assert.Equal("newText não pode ser nulo, vazio ou texto em branco. (Parameter 'newText')", ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstStringNullOrWhiteSpaceCaseWhiteSpace(){
            try{
                string newText = "     ";
                CustomGuards.AgainstStringNullOrWhiteSpace(newText, nameof(newText));
                Assert.Fail("Guarda precisa lançar exception caso string esteja preenchida com espaço em branco");
            }
            catch(ArgumentException ex){
                Assert.Equal("newText não pode ser nulo, vazio ou texto em branco. (Parameter 'newText')", ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstStringNullOrWhiteSpaceCaseStringWithValue(){
            try{
                string newText = "string com valor";
                CustomGuards.AgainstStringNullOrWhiteSpace(newText, nameof(newText));
            }
            catch(Exception ex){
                Assert.Fail($"Guarda lançou exception para uma string preenchida. {ex.Message}");
            }
        }

        [Fact]
        public void AgainstNegativeOrZeroValueCaseNegative()
        {
            try{
                decimal negativeValue = -100;
                CustomGuards.AgainstNegativeOrZeroValue(negativeValue, nameof(negativeValue));
                Assert.Fail("Guarda precisa lançar exception caso valor decimal menor que zero");
            }
            catch(ArgumentException ex){
                Assert.Equal("negativeValue precisa ser maior do que zero. (Parameter 'negativeValue')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstNegativeOrZeroValueCaseZero(){
            try{
                decimal zeroValue = 0;
                CustomGuards.AgainstNegativeOrZeroValue(zeroValue, nameof(zeroValue));
                Assert.Fail("Guarda precisa lançar exception caso valor decimal igual a zero");
            }
            catch(ArgumentException ex){
                Assert.Equal("zeroValue precisa ser maior do que zero. (Parameter 'zeroValue')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda precisa lançar exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstNegativeOrZeroValueCaseHigherThanZero(){
            try{
                decimal higherValue = 10;
                CustomGuards.AgainstNegativeOrZeroValue(higherValue, nameof(higherValue));
            }
            catch(Exception){
                Assert.Fail("Guarda lançou exception com um valor decimal maior que zero");
            }
        }

        [Fact]
        public void AgainstObjectNullWithNullObject(){
            try{
                string? objeto = null;
                CustomGuards.AgainstObjectNull(objeto!, nameof(objeto));
                Assert.Fail("Guarda deveria lançar exception em caso de objeto nulo");
            }
            catch(ArgumentException ex){
                Assert.Equal("objeto não pode ser nulo. (Parameter 'objeto')",ex.Message);
            }
            catch(Exception){
                Assert.Fail("Guarda deveria lançar exception do tipo ArgumentException");
            }
        }

        [Fact]
        public void AgainstObjectWithValue(){
            try{
                string objeto = "MeuObjetoRepresentadoPorString";
                CustomGuards.AgainstObjectNull(objeto, nameof(objeto));
            }
            catch(Exception){
                Assert.Fail("Guarda lançou exception com objeto que existe.");
            }
        }
    }
}