using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankFunctionsApp.FunctionApp.Functions
{
    public static class ProcessTransferFunction
    {
        [FunctionName("ProcessTransfer")]
        public static async Task Run(
            [QueueTrigger("fund-transfers", Connection = "AzureWebJobsStorage")] string message,
            ILogger log)
        {
            log.LogInformation("ProcessTransfer triggered.");
            var transfer = JsonConvert.DeserializeObject<TransferRequest>(message);

            var loggerFactory = new LoggerFactory();
            var accountService = new AccountService(loggerFactory);
            var notify = new NotificationService(loggerFactory);

            try
            {
                // NOTE: Real system must implement distributed transaction or ledger, idempotency and audit.
                var debitOk = await accountService.DebitAsync(transfer.FromAccountId, transfer.Amount);
                if (!debitOk) throw new Exception("Debit failed");

                var creditOk = await accountService.CreditAsync(transfer.ToAccountId, transfer.Amount);
                if (!creditOk) throw new Exception("Credit failed");

                // Notify both parties
                await notify.SendEmailAsync("from@example.com", "Debit Notification", $"Debited {transfer.Amount}");
                await notify.SendEmailAsync("to@example.com", "Credit Notification", $"Credited {transfer.Amount}");

                log.LogInformation($"Transfer {transfer.TransferId} processed successfully.");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"Transfer {transfer?.TransferId} failed.");
                // TODO: move message to poison queue or retry logic
                throw;
            }
        }
    }
}
