using Microsoft.AspNetCore.Http;
using System;

namespace TelaLogin.ExceptionResponse
{
    public class NaoEncontradoException:Exception
    {
        public int StatusCode { get; set; }
        public override string Message { get; }
        public NaoEncontradoException(string message=null, int statusCode = StatusCodes.Status404NotFound):base(message)
        {
            if (message == null|| message == string.Empty)
            {
            message = "Não Encontrado";

            }
            StatusCode = statusCode;
            Message = message;
        }
       
    }
}
