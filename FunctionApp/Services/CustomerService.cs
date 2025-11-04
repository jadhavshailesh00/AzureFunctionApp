using BankFunctionsApp.FunctionApp.Models;
using Microsoft.Extensions.Logging;

namespace BankFunctionsApp.FunctionApp.Services
{
    public class CustomerService
    {
        private readonly ILogger _log;
        public CustomerService(ILoggerFactory loggerFactory) { _log = loggerFactory.CreateLogger<CustomerService>(); }

        public Task SaveCustomerAsync(Customer c)
        {
            _log.LogInformation($"[CustomerService] Saving customer {c.Email} / {c.id}");
            // TODO: persist to CosmosDB / SQL
            return Task.CompletedTask;
        }
    }
}
