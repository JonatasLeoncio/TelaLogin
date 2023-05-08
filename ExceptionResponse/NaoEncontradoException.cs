using Microsoft.AspNetCore.Http;
using System;

namespace TelaLogin.ExceptionResponse
{
    public class NaoEncontradoException:Exception
    {
        public int StatusCode { get; set; }
        public NaoEncontradoException(string message, int statusCode = StatusCodes.Status404NotFound):base(message)
        {
            StatusCode = statusCode;
        }
       
    }
}
