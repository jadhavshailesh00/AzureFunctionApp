using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BankFunctionsApp.FunctionApp.Services
{
    public class AccountService
    {
        private readonly ILogger _log;
        public AccountService(ILoggerFactory loggerFactory) { _log = loggerFactory.CreateLogger<AccountService>(); }

        public Task<bool> DebitAsync(string accountId, decimal amount)
        {
            _log.LogInformation($"[AccountService] Debiting {amount} from {accountId}");
            // TODO: implement idempotency, concurrency, ACID operations
            return Task.FromResult(true);
        }

        public Task<bool> CreditAsync(string accountId, decimal amount)
        {
            _log.LogInformation($"[AccountService] Crediting {amount} to {accountId}");
            return Task.FromResult(true);
        }

        public Task<decimal> GetBalanceAsync(string accountId)
        {
            return Task.FromResult(10000m); // stub
        }
    }
}
