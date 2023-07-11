namespace TestCase.Application.Interfaces
{
    public interface IHangfireService
    {
        Task SendReminderEmailAsync();
    }
}
