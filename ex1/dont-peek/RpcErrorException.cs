using System;

namespace solution
{
    public class RpcErrorException : Exception
    {
        public int ErrorCode { get; }
        public string ErrorMessage { get; }

        public RpcErrorException(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
