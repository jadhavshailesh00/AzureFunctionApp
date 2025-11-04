using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BankFunctionsApp.FunctionApp.Services
{
    public class PdfStatementService
    {
        private readonly ILogger _log;
        public PdfStatementService(ILoggerFactory loggerFactory) { _log = loggerFactory.CreateLogger<PdfStatementService>(); }

        public Task<string> GenerateMonthlyStatementPdfAsync(string accountId, int year, int month)
        {
            _log.LogInformation($"[PdfStatement] Generating statement for {accountId} {year}-{month}");
            // TODO: create real PDF and store to blob storage
            return Task.FromResult($"statements/{accountId}-{year}-{month}.pdf");
        }
    }
}
