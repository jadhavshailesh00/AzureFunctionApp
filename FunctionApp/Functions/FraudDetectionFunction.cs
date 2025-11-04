using BankFunctionsApp.FunctionApp.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BankFunctionsApp.FunctionApp.Functions
{
    public static class FraudDetectionFunction
    {
        [FunctionName("FraudDetection")]
        public static async Task Run(
            [EventHubTrigger("transactions", Connection = "EventHubConnection")] string[] events,
            ILogger log)
        {
            var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
            var notify = new Services.NotificationService(loggerFactory);

            foreach (var e in events)
            {
                var tx = JsonConvert.DeserializeObject<TransactionEvent>(e);
                if (tx == null) continue;

                // Simple rules example
                if (tx.Amount > 5000m || (tx.Location != null && tx.Location != tx.HomeLocation))
                {
                    log.LogWarning($"Fraud suspicion for tx {tx.TransactionId} amount {tx.Amount} location {tx.Location}");
                    await notify.SendSmsAsync("+999999999", $"Fraud alert: {tx.TransactionId} amount {tx.Amount}");
                    // TODO: escalate: block card, open investigation, write to fraud DB
                }
            }
        }
    }

}
