using Microsoft.Extensions.Logging;
using SnackTech.Domain.Common;

namespace SnackTech.Application.UseCases
{
    public abstract class BaseService(ILogger logger)
    {
        private readonly ILogger logger = logger;

        public async Task<Result> CommonExecution(string nomeMetodo, Func<Task<Result>> processo){
            try{
                return await Task.Run(processo);
            }
            catch(ArgumentException aex)
            {
                logger.LogWarning(aex,"{NomeMetodo} - ArgumentException - {Message}",nomeMetodo,aex.Message);
                return new Result(aex.Message);
            }
            catch(Exception ex){
                logger.LogError(ex,"{NomeMetodo} - Exception - {Message}",nomeMetodo,ex.Message);
                return new Result(ex);
            }
        }

        public async Task<Result<T>> CommonExecution<T>(string nomeMetodo, Func<Task<Result<T>>> processo){
            try{
                return await Task.Run(processo);
            }
             catch(ArgumentException aex)
            {
                logger.LogWarning(aex,"{NomeMetodo} - ArgumentException - {Message}",nomeMetodo,aex.Message);
                return new Result<T>(aex.Message,true);
            }
            catch(Exception ex){
                logger.LogError(ex,"{NomeMetodo} - Exception - {Message}",nomeMetodo,ex.Message);
                return new Result<T>(ex);
            }
        }
    }
}