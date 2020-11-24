using System.Collections.Generic;
using System.Threading.Tasks;
using TennisBookings.Web.Core;

namespace TennisBookings.Web.IntegrationTests.Fakes
{
    public class FakeEmailService : IEmailService
    {
        public List<Email> SentEmails = new List<Email>();

        public Task SendAsync(string sender, string recipient, string title, string content)
        {
            SentEmails.Add(new Email(sender, recipient,title, content));
            return Task.CompletedTask;
        }

        public class Email
        {
            public string Sender { get; }
            public string Recipient { get; }
            public string Title { get; }
            public string Content { get; }

            public Email(string sender, string recipient, string title, string content)
            {
                Sender = sender;
                Recipient = recipient;
                Title = title;
                Content = content;
            }
        }
    }
}
