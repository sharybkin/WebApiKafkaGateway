using System;

namespace ProductApi.Exceptions
{
    public class GatewayException : Exception
    {
        public GatewayException(string message) : base(message) 
        {
            
        }
    }
}