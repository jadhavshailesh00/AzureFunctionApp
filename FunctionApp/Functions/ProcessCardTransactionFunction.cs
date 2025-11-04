using BankFunctionsApp.FunctionApp.Models;
using BankFunctionsApp.FunctionApp.Services;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BankFunctionsApp.FunctionApp.Functions
{
    public static class ProcessCardTransactionFunction
    {
        [FunctionName("ProcessCardTransaction")]
        public static async Task Run(
            [ServiceBusTrigger("card-transactions", "bank-subscriber", Connection = "ServiceBusConnection")] string message,
            ILogger log)
        {
            log.LogInformation("ProcessCardTransaction triggered.");
            var tx = JsonConvert.DeserializeObject<CardTransaction>(message);

            var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
            var accountService = new AccountService(loggerFactory);
            var notify = new NotificationService(loggerFactory);

            // Validate card, limit checks, etc.
            await accountService.DebitAsync(tx.CardId, tx.Amount);

            // Push notification
            await notify.SendSmsAsync("+100000000", $"Card charged {tx.Amount} at {tx.Merchant}");
            log.LogInformation($"Card transaction {tx.TransactionId} processed.");
        }
    }
}
