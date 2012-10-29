using System.Collections.Generic;

namespace FluentMail
{
    public interface IEmail
    {
        string FromAddress { get; }
        string Subject { get; }
        string Body { get; }

        bool IsHtml { get; }

        IList<string> ToAddresses { get; }
        IList<string> CcAddresses { get; }
        IList<string> BccAddresses { get; }
    }
}