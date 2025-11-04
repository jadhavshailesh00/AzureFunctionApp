using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading.Tasks;
using FunctionApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BankFunctionsApp.FunctionApp.Functions
{
    public static class InitiateTransferFunction
    {
        [FunctionName("InitiateTransfer")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "transfers/initiate")] HttpRequest req,
            [Queue("fund-transfers", Connection = "AzureWebJobsStorage")] IAsyncCollector<string> transferQueue,
            ILogger log)
        {
            log.LogInformation("InitiateTransfer called.");

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var transfer = JsonConvert.DeserializeObject<TransferRequest>(body);

            if (transfer == null) return new BadRequestObjectResult("Invalid payload.");
            if (transfer.Amount <= 0) return new BadRequestObjectResult("Amount must be > 0.");

            await transferQueue.AddAsync(JsonConvert.SerializeObject(transfer));
            return new OkObjectResult(new { message = "Transfer queued", transferId = transfer.TransferId });
        }
    }
}
