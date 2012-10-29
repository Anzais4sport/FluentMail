using System;
using System.Collections.Generic;
using System.Linq;
using FluentMail.Transport;
using FluentMail.Validation;
using FluentMail.Configuration;

namespace FluentMail
{
    public class EmailMessage : IEmailBuilder, IEmail
    {
        public string FromAddress { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        public bool IsHtml { get; private set; }

        public IList<string> ToAddresses { get; private set; }
        public IList<string> CcAddresses { get; private set; }
        public IList<string> BccAddresses { get; private set; }

        public IEmailBuilder From(string address)
        {
            FromAddress = address;

            return this;
        }

        public IEmailBuilder To(params string[] addresses)
        {
            if (ToAddresses == null)
                ToAddresses = new List<string>();

            foreach (string address in addresses)
                if (!ToAddresses.Contains(address))
                    ToAddresses.Add(address);

            return this;
        }

        public IEmailBuilder Cc(params string[] addresses)
        {
            if (CcAddresses == null)
                CcAddresses = new List<string>();

            foreach (string address in addresses)
                if (!CcAddresses.Contains(address))
                    CcAddresses.Add(address);

            return this;
        }

        public IEmailBuilder Bcc(params string[] addresses)
        {
            if (BccAddresses == null)
                BccAddresses = new List<string>();

            foreach (string address in addresses)
                if (!BccAddresses.Contains(address))
                    BccAddresses.Add(address);

            return this;
        }

        public IEmailBuilder WithSubject(string subject)
        {
            Subject = subject;

            return this;
        }

        public IEmailBuilder WithBody(string body)
        {
            Body = body;

            return this;
        }

        public IEmailBuilder AsHtml()
        {
            IsHtml = true;

            return this;
        }

        public void Send()
        {
            ValidateRequired();
            ValidateAddresses();

            SendMessage();
        }

        private void ValidateRequired()
        {
            if (string.IsNullOrEmpty(FromAddress))
                throw new IncompleteEmailException("From address cannot be null.");

            if (ToAddresses == null || ToAddresses.Count == 0)
                throw new IncompleteEmailException(
                    "Email should have at least one to address.");

            if (string.IsNullOrEmpty(Subject))
                throw new IncompleteEmailException("Subject cannot be null.");

            if (string.IsNullOrEmpty(Body))
                throw new IncompleteEmailException("Body cannot be null.");
        }

        private static IEmailAddressValidator GetEmailAddressValidatorFromConfiguration()
        {
            var config = (FluentMailSection)
                System.Configuration.ConfigurationManager.GetSection("fluentMail");

            return (IEmailAddressValidator) Activator.CreateInstance(config.EmailAddressValidator.Type);
        }

        private void ValidateAddresses() {
            var emailAddressValidator = GetEmailAddressValidatorFromConfiguration();

            if (!emailAddressValidator.Validate(FromAddress))
                throw new InvalidEmailAddressException("From: " + FromAddress);

            if (ToAddresses.Count > 0)
                foreach (var address in ToAddresses.Where(address => !emailAddressValidator.Validate(address)))
                    throw new InvalidEmailAddressException("To: " + address);

            if (CcAddresses != null && CcAddresses.Count > 0)
                foreach (var address in CcAddresses.Where(address => !emailAddressValidator.Validate(address)))
                    throw new InvalidEmailAddressException("Cc: " + address);

            if (BccAddresses != null && BccAddresses.Count > 0)
                foreach (var address in BccAddresses.Where(address => !emailAddressValidator.Validate(address)))
                    throw new InvalidEmailAddressException("Bcc: " + address);
        }

        private static IPostalService GetPostalServiceFromConfiguration()
        {
            var config = (FluentMailSection)
                System.Configuration.ConfigurationManager.GetSection("fluentMail");

            return (IPostalService) Activator.CreateInstance(config.PostalService.Type);
        }

        private void SendMessage()
        {
            var postalService = GetPostalServiceFromConfiguration();

            try
            {
                postalService.Send(this);
            }
            catch (Exception e)
            {
                throw new EmailTransportException(
                    "Email could not be sent: " + e.Message, e);
            }
        }
    }
}