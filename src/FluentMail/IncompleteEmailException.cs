using System;

namespace FluentMail
{
    public class IncompleteEmailException : Exception
    {
        public IncompleteEmailException(string message) 
            : base(message)
        {
            
        }
    }
}