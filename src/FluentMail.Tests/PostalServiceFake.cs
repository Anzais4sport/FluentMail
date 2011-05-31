using FluentMail.Transport;

namespace FluentMail.Tests
{
    public class PostalServiceFake : IPostalService
    {
        public static int SendCount { get; set; }

        public void Send(IEmail email)
        {
            SendCount += 1;
        }
    }
}
