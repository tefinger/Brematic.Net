using System;

namespace Brematic.Net.Exceptions
{
    public class InvalidCodeException : Exception
    {
        public InvalidCodeException() : base("Invalid value in system or unit code!") { }
    }
}