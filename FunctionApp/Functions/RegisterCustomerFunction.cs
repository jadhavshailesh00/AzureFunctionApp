using BankFunctionsApp.FunctionApp.Models;
using BankFunctionsApp.FunctionApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BankFunctionsApp.FunctionApp.Functions
{
    public static class RegisterCustomerFunction
    {
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "customer/register")] HttpRequest httpRequest,
            ILogger log)
        {
            log.LogInformation("RegisterCustomer called.");
            try
            {
                var requestBody = await new StreamReader(httpRequest.Body).ReadToEndAsync();
                var customerData = System.Text.Json.JsonSerializer.Deserialize<Customer>(requestBody) ?? new Customer();

                if (string.IsNullOrWhiteSpace(customerData.Email))
                    return new BadRequestObjectResult("Email is required.");

                var svc = new CustomerService(new LoggerFactory());
                await svc.SaveCustomerAsync(customerData);

                return new OkObjectResult(new { message = "Customer registered", customerId = customerData.id });

            }
            catch (Exception ex)
            {
                log.LogError($"Error registering customer: {ex.Message}");
                return new StatusCodeResult(500);
            }

        );
        }
    }
}
