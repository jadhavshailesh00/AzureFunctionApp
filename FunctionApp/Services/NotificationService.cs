using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BankFunctionsApp.FunctionApp.Services
{
    public class NotificationService
    {
        private readonly ILogger _log;
        public NotificationService(ILoggerFactory loggerFactory) { _log = loggerFactory.CreateLogger<NotificationService>(); }

        public Task SendEmailAsync(string email, string subject, string body)
        {
            _log.LogInformation($"[Notification] Email to {email}: {subject}");
            return Task.CompletedTask;
        }

        public Task SendSmsAsync(string phone, string message)
        {
            _log.LogInformation($"[Notification] SMS to {phone}: {message}");
            return Task.CompletedTask;
        }
    }
}
