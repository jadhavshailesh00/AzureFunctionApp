using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankFunctionsApp.FunctionApp.Functions
{
    public static class CheckDepositFunction
    {
        [FunctionName("CheckDeposit")]
        public static async Task Run(
            [BlobTrigger("checks/{name}", Connection = "AzureWebJobsStorage")] Stream image,
            string name,
            ILogger log)
        {
            log.LogInformation($"Check deposit blob received: {name}, length={image.Length}");

            var loggerFactory = new Microsoft.Extensions.Logging.LoggerFactory();
            // TODO: call OCR service (Azure Cognitive Services Computer Vision / Form Recognizer)
            // Here we simulate extraction:
            await Task.Delay(200); // simulate processing

            log.LogInformation($"Extracted amount: 123.45 from check {name}");
            // TODO: create deposit transaction, update ledger, notify user
        }
    }
}
