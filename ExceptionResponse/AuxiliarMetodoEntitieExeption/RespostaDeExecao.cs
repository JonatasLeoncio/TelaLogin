using System;

namespace TelaLogin.ExceptionResponse.AuxiliarMetodoEntitieExeption
{
    public class RespostaDeExecao
    {
        static public EntitieExceptionResponse VerificaException(Exception ex)
        {
            if (ex is NaoEncontradoException naoEncontradoException)
            {
                EntitieExceptionResponse execaoResponse = new EntitieExceptionResponse();
                execaoResponse.Status = naoEncontradoException.StatusCode;
                execaoResponse.Message = naoEncontradoException.Message;
                return execaoResponse;
            }

            return null;
        }
    }
}
