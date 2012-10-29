namespace FluentMail.Transport
{
    public interface IPostalService
    {
        void Send(IEmail email);
    }
}