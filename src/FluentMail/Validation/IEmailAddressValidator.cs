namespace FluentMail.Validation
{
    public interface IEmailAddressValidator
    {
        /// <summary>
        /// Checks if an e-mail address is valid.
        /// </summary>
        /// <param name="emailAddress">The address to validate.</param>
        /// <returns>true if the address is valid, false otherwise.</returns>
        bool Validate(string emailAddress);
    }
}