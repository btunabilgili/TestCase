namespace TestCase.Application.Interfaces
{
    public interface IFakeEmailService
    {
        Task SendEmailAsync(string senderEmail, string subject, string body, IEnumerable<string> recipients, IEnumerable<string>? ccList = null, IEnumerable<string>? bccList = null);
    }
}
