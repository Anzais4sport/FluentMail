namespace FluentMail
{
    public interface IEmailBuilder
    {
        IEmailBuilder From(string address);
        IEmailBuilder To(params string[] addresses);
        IEmailBuilder Cc(params string[] addresses);
        IEmailBuilder Bcc(params string[] addresses);
        
        IEmailBuilder WithSubject(string subject);
        IEmailBuilder WithBody(string body);

        IEmailBuilder AsHtml();

        void Send();
    }
}