using System;

namespace Brematic.Net.Exceptions
{
    public class UnsupportedGatewayException : Exception
    {
        public UnsupportedGatewayException() : base("Unsupported gateway") { }
    }
}