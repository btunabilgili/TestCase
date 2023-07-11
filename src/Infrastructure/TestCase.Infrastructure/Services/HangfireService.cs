using Microsoft.Extensions.Logging;
using TestCase.Application.Interfaces;

namespace TestCase.Infrastructure.Services
{
    public class HangfireService : IHangfireService
    {
        private readonly IFakeEmailService _fakeEmailService;
        private readonly ILogger<HangfireService> _logger;
        public HangfireService(IFakeEmailService fakeEmailService, ILogger<HangfireService> logger)
        {
            _fakeEmailService = fakeEmailService ?? throw new ArgumentNullException(nameof(fakeEmailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendReminderEmailAsync()
        {
            _logger.LogInformation("Reminder email send process started");

            var recipients = new List<string>
            {
                "test@test.com"
            };

            await _fakeEmailService.SendEmailAsync("testcase@test.com", "Reminder", "Job", recipients);

            _logger.LogInformation("Reminder emain send process finished");
        }
    }
}
