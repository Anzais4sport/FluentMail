using System.Net.Mail;

namespace FluentMail.Transport
{
    public class PostalService : IPostalService
    {
        public void Send(IEmail email)
        {
            var mailMessage = new MailMessage {
                From = new MailAddress(email.FromAddress),
                Subject = email.Subject, Body = email.Body, IsBodyHtml = email.IsHtml
            };

            if (email.ToAddresses.Count > 0)
                foreach (var address in email.ToAddresses)
                    mailMessage.To.Add(address);

            if (email.CcAddresses != null && email.CcAddresses.Count > 0)
                foreach (var address in email.CcAddresses)
                    mailMessage.CC.Add(address);

            if (email.BccAddresses != null && email.BccAddresses.Count > 0)
                foreach (var address in email.BccAddresses)
                    mailMessage.Bcc.Add(address);

            mailMessage.Headers.Add("X-Mailer", "Fluent Mail");

            new SmtpClient().Send(mailMessage);
        }
    }
}