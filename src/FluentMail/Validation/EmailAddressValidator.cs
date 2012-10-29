using System.Text.RegularExpressions;

namespace FluentMail.Validation
{
    public class EmailAddressValidator : IEmailAddressValidator
    {
        /// <summary>
        /// Checks if an e-mail address is valid.
        /// </summary>
        /// <param name="emailAddress">The address to validate.</param>
        /// <returns>true if the address is valid, false otherwise.</returns>
        public bool Validate(string emailAddress)
        {
            if (string.IsNullOrEmpty(emailAddress))
                return false;

            return Regex.IsMatch(emailAddress, 
                @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }

    }
}