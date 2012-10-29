using System;

namespace FluentMail.Transport
{
    public class EmailTransportException : ApplicationException
    {
        public EmailTransportException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}