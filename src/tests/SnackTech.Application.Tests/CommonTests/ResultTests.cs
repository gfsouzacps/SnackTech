using SnackTech.Domain.Common;

namespace SnackTech.Application.Tests.CommonTests
{
    public class ResultTests
    {
        [Fact]
        public void CreateResultWithSuccess()
        {
            var resultado = new Result();
            Assert.True(resultado.IsSuccess());
            Assert.True(resultado.Success);
        }

        [Fact]
        public void CreateResultWithMessage()
        {
            var resultado = new Result("mensagem");
            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Message);
            Assert.Equal("mensagem", resultado.Message);
        }

        [Fact]
        public void CreateResultWithException()
        {
            var exception = new Exception("Exception lançada");
            var resultado = new Result(exception);
            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Message);
            Assert.True(resultado.HasException());
            Assert.NotNull(resultado.Exception);
            Assert.Equal("Exception lançada", resultado.Exception.Message);
            Assert.Equal("Exception lançada", resultado.Message);
        }

        [Fact]
        public void CreateResultWithData()
        {
            var resultado = new Result<int>(10);
            Assert.True(resultado.IsSuccess());
            Assert.Equal(10, resultado.Data);
            Assert.Equal(10, resultado.GetValue());
        }

        [Fact]
        public void CreateResultWithObjectData()
        {
            var resultado = new Result<string>("string como objeto de teste");
            Assert.True(resultado.IsSuccess());
            Assert.NotNull(resultado.Data);
            Assert.Equal(string.Empty, resultado.Message);
            Assert.Equal("string como objeto de teste", resultado.Data);
            Assert.Equal("string como objeto de teste", resultado.GetValue());
        }

        [Fact]
        public void CreateResultOfTButWithMessage()
        {
            var resultado = new Result<bool>("Representa algum erro lógico", true);
            Assert.False(resultado.IsSuccess());
            Assert.Equal(default, resultado.Data);
            Assert.NotNull(resultado.Message);
            Assert.Equal("Representa algum erro lógico", resultado.Message);
        }

        [Fact]
        public void CreateResultOfTButWithException()
        {
            var exception = new Exception("Erro inesperado");
            var resultado = new Result<bool>(exception);
            Assert.False(resultado.IsSuccess());
            Assert.Equal(default, resultado.Data);
            Assert.NotNull(resultado.Message);
            Assert.NotNull(resultado.Exception);
            Assert.True(resultado.HasException());
            Assert.Equal("Erro inesperado", resultado.Exception.Message);
            Assert.Equal("Erro inesperado", resultado.Message);
        }

        [Fact]
        public void CreateResultOfTWithBooleanFalse()
        {
            try
            {
                Result<int> resultado = new("Mensagem do resultado", false);
                resultado.IsSuccess();
                Assert.Fail("Deveria ter lançado Exception");
            }
            catch (ArgumentException ex)
            {
                Assert.Equal("Use Result<string>(string) como construtor para resultados de sucesso. (Parameter 'isError')", ex.Message);
            }
            catch (Exception)
            {
                Assert.Fail("Deveria ter lançado ArgumentException");
            }

        }
    }
}