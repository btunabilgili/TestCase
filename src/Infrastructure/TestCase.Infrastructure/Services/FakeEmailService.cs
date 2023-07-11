using TestCase.Application.Interfaces;

namespace TestCase.Infrastructure.Services
{
    public class FakeEmailService : IFakeEmailService
    {
        public Task SendEmailAsync(string senderEmail, string subject, string body, IEnumerable<string> recipients, IEnumerable<string>? ccList = null, IEnumerable<string>? bccList = null)
        {
            // Send email

            return Task.CompletedTask;
        }
    }
}
