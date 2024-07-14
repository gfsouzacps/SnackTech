namespace SnackTech.Application.Common
{
    public class Result
    {
        public bool Success {get; protected set;}
        public string Message {get; protected set;}
        public Exception Exception {get; protected set;}

        public Result()
        {
            Success = true;
            Message = string.Empty;
            Exception = null!;
        }

        public Result(string message){
            Success = false;
            Message = message;
            Exception = null!;
        }

        public Result(Exception exception){
            Success = false;
            Message = exception.Message;
            Exception = exception;
        }

        public bool IsSuccess() => Success;
        public bool HasException() => Exception != null;
    }

    public class Result<T> : Result{
        public T Data {get; protected set;}

        public Result(T data)
            :base()
        {
            Data = data;
            Success = true;
        }

        public Result(string message, bool isError): base(message){
            if(!isError){
                var mensagem = "Use Result<string>(string) como construtor para resultados de sucesso.";
                throw new ArgumentException(mensagem,nameof(isError));
            }
            Data = default!;
        }

        public Result(Exception exception)
            :base(exception)
        {
            Data = default!;
        }

        public T GetValue() => Data;
    }
}