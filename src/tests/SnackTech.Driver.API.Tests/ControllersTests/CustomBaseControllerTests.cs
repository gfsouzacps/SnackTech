// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using Moq;
// using SnackTech.Driver.API.Controllers;
// using SnackTech.Driver.API.CustomResponses;
// using SnackTech.Domain.Common;

// namespace SnackTech.Driver.API.Tests.ControllersTests
// {
//     public class CustomBaseControllerTests
//     {
//         private readonly Mock<ILogger> logger;
//         private readonly Mock<CustomBaseController> mockBaseController;
//         private readonly CustomBaseController baseController;

//         public CustomBaseControllerTests(){
//             logger = new Mock<ILogger>();
//             mockBaseController = new Mock<CustomBaseController>(logger.Object)
//             {
//                 CallBase = true
//             };
//             baseController = mockBaseController.Object;
//         }

//         [Fact]
//         public async Task CommonExecutionWithSuccess()
//         {
//             var nomeMetodo = "Controller.Nome";
//             static async Task<Result<int>> taskFunc()
//             {
//                 await Task.FromResult(0);
//                 return new Result<int>(10);
//             }

//             var task = taskFunc();

//             var resultado = await baseController.CommonExecution(nomeMetodo, task);

//             Assert.IsType<OkObjectResult>(resultado);

//         }

//         [Fact]
//         public async Task CommonExecutionReturningBadRequest()
//         {
//             var nomeMetodo = "Controller.Nome";

//             static async Task<Result<int>> taskFunc()
//             {
//                 await Task.FromResult(0);
//                 return new Result<int>("Erro de lógica", true);
//             }

//             var task = taskFunc();

//             var resultado = await baseController.CommonExecution(nomeMetodo, task);
//             var requestResult = Assert.IsType<BadRequestObjectResult>(resultado);
//             var payload = Assert.IsType<ErrorResponse>(requestResult.Value);
//             Assert.Null(payload.Exception);
//             Assert.Equal("Erro de lógica", payload.Message);
//         }

//         [Fact]
//         public async Task CommonExecutionReturningInternalServerErroFromTask()
//         {
//             var nomeMetodo = "Controller.Nome";

//             static async Task<Result<int>> taskFunc()
//             {
//                 await Task.FromResult(0);
//                 return new Result<int>(new Exception("Erro inesperado"));
//             }

//             var task = taskFunc();

//             var resultado = await baseController.CommonExecution(nomeMetodo, task);
//             var requestResult = Assert.IsType<ObjectResult>(resultado);
//             var payload = Assert.IsType<ErrorResponse>(requestResult.Value);
//             Assert.NotNull(payload);
//             Assert.Equal("Erro inesperado", payload.Message);
//         }

//         [Fact]
//         public async Task CommonExecutionReturningInternalServerErrorFromProcessing()
//         {
//             var nomeMetodo = "Controller.Nome";

//             static async Task<Result<int>> taskFunc()
//             {
//                 await Task.FromResult(0);
//                 throw new Exception("Erro inesperado");
//             }

//             var task = taskFunc();

//             var resultado = await baseController.CommonExecution(nomeMetodo, task);
//             var requestResult = Assert.IsType<ObjectResult>(resultado);
//             var payload = Assert.IsType<ErrorResponse>(requestResult.Value);
//             Assert.NotNull(payload);
//             Assert.Equal("Erro inesperado", payload.Message);
//         }
//     }
// }