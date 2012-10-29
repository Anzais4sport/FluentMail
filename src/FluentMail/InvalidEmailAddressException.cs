using System;

namespace FluentMail
{
    public class InvalidEmailAddressException : Exception
    {
        public InvalidEmailAddressException(string message)
            : base(message)
        {
            
        }
    }
}