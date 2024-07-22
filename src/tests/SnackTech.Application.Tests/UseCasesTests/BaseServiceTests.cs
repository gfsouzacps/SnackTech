using Microsoft.Extensions.Logging;
using Moq;
using SnackTech.Application.Common;
using SnackTech.Application.UseCases;

namespace SnackTech.Application.Tests.UseCasesTests
{
    public class BaseServiceTests
    {
        private readonly Mock<ILogger> logger;

        public BaseServiceTests(){
            logger = new Mock<ILogger>();
        }

        [Fact]
        public async Task CommonExecutionWithSuccess(){
            var mock = new Mock<BaseService>(logger.Object);
            mock.CallBase = true;
            var classe = mock.Object;

            async Task<Result> processamento(){
                await Task.FromResult(0);
                return new Result();
            }

            var resultado = await classe.CommonExecution("NomeMetodo",processamento);

            Assert.True(resultado.IsSuccess());
        }

        [Fact]
        public async Task CommonExecutionWithArgumentException(){
            var mock = new Mock<BaseService>(logger.Object);
            mock.CallBase = true;
            var classe = mock.Object;

            async Task<Result> processamento(){
                await Task.FromResult(0);
                throw new ArgumentException("Erro de parametrização");
            }

            var resultado = await classe.CommonExecution("NomeMetodo",processamento);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Message);
            Assert.Contains("Erro de parametrização",resultado.Message);

            logger.Verify(l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state,type) => (state.ToString() ?? "").Contains("NomeMetodo - ArgumentException")),
                It.IsAny<Exception>(),
                (Func<object, Exception?, string>)It.IsAny<object>()
            ), Times.Once);
        }

        [Fact]
        public async Task CommonExecutionWithException(){
            var mock = new Mock<BaseService>(logger.Object);
            mock.CallBase = true;
            var classe = mock.Object;

            async Task<Result> processamento(){
                await Task.FromResult(0);
                throw new Exception("Erro inesperado");
            }

            var resultado = await classe.CommonExecution("NomeMetodo",processamento);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Equal("Erro inesperado",resultado.Exception.Message);

            logger.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state,type) => (state.ToString() ?? "").Contains("NomeMetodo - Exception")),
                It.IsAny<Exception>(),
                (Func<object, Exception?, string>)It.IsAny<object>()
            ), Times.Once);
        }

        [Fact]
        public async Task CommonExecutionOfTWithSuccess(){
            var mock = new Mock<BaseService>(logger.Object);
            mock.CallBase = true;
            var classe = mock.Object;

            async Task<Result<int>> processamento(){
                await Task.FromResult(0);
                return new Result<int>(10);
            }

            var resultado = await classe.CommonExecution("NomeMetodo",processamento);

            Assert.True(resultado.IsSuccess());
        }

        [Fact]
        public async Task CommonExecutionOfTWithArgumentException(){
            var mock = new Mock<BaseService>(logger.Object);
            mock.CallBase = true;
            var classe = mock.Object;

            async Task<Result<int>> processamento(){
                await Task.FromResult(0);
                throw new ArgumentException("Erro de parametrização");
            }

            var resultado = await classe.CommonExecution("NomeMetodo",processamento);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Message);
            Assert.Contains("Erro de parametrização",resultado.Message);

            logger.Verify(l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state,type) => (state.ToString() ?? "").Contains("NomeMetodo - ArgumentException")),
                It.IsAny<Exception>(),
                (Func<object, Exception?, string>)It.IsAny<object>()
            ), Times.Once);
        }

        [Fact]
        public async Task CommonExecutionOfTWithException(){
            var mock = new Mock<BaseService>(logger.Object);
            mock.CallBase = true;
            var classe = mock.Object;

            async Task<Result<int>> processamento(){
                await Task.FromResult(0);
                throw new Exception("Erro inesperado");
            }

            var resultado = await classe.CommonExecution("NomeMetodo",processamento);

            Assert.False(resultado.IsSuccess());
            Assert.NotNull(resultado.Exception);
            Assert.Equal("Erro inesperado",resultado.Exception.Message);

            logger.Verify(l => l.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((state,type) => (state.ToString() ?? "").Contains("NomeMetodo - Exception")),
                It.IsAny<Exception>(),
                (Func<object, Exception?, string>)It.IsAny<object>()
            ), Times.Once);
        }
    }
}