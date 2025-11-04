using BankFunctionsApp.FunctionApp.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;



namespace BankFunctionsApp.Functions;

public class ProcessTransferFunction
{
    private readonly ILogger _logger;

    public ProcessTransferFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ProcessTransferFunction>();
    }


    [Function("ProcessTransfer")]
    public void Run(
        [QueueTrigger("transactions", Connection = "AzureWebJobsStorage")] string message)
    {
        var transfer = JsonConvert.DeserializeObject<TransferRequest>(message);

        _logger.LogInformation(
            $"Processing transfer: {transfer.FromAccountId} -> {transfer.ToAccountId}, Amount: {transfer.Amount}"
        );
    }
}
