using BankFunctionsApp.FunctionApp.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BankFunctionsApp.FunctionApp.Functions
{
    public static class GenerateStatementsFunction
    {
        // Runs on day 1 of month at 00:00 UTC
        [FunctionName("GenerateStatements")]
        public static async Task Run([TimerTrigger("0 0 0 1 * *")] TimerInfo timer, ILogger log)
        {
            var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
            var pdfService = new PdfStatementService(loggerFactory);
            var notify = new NotificationService(loggerFactory);

            log.LogInformation($"GenerateStatements executed at: {DateTime.UtcNow}");

            // For demo: iterate a small set of account IDs
            var accounts = new[] { "acct-1001", "acct-1002" };
            var now = DateTime.UtcNow;
            foreach (var acct in accounts)
            {
                var path = await pdfService.GenerateMonthlyStatementPdfAsync(acct, now.Year, now.Month);
                // TODO: Upload path to blob storage and send link
                await notify.SendEmailAsync("customer@example.com", "Your monthly statement is ready",
                    $"Statement available at: {path}");
            }
        }
    }
}
