using System;

namespace TelaLogin.ExceptionResponse.AuxiliarMetodoEntitieExeption
{
    public class RespostaDeExecao
    {
        static public EntitieExceptionResponse VerificaException(Exception ex)
        {
            EntitieExceptionResponse execaoResponse = new EntitieExceptionResponse();
            if (ex is NaoEncontradoException naoEncontradoException)
            {               
                execaoResponse.Status = naoEncontradoException.StatusCode;
                execaoResponse.Message = naoEncontradoException.Message;
                return execaoResponse;
            }
            return null;
        }
    }
}
