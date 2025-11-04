using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace BankFunctionsApp.Functions;

public class RegisterCustomerFunction
{
    private readonly ILogger _logger;

    public RegisterCustomerFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<RegisterCustomerFunction>();
    }

    [Function("RegisterCustomer")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var data = JsonConvert.DeserializeObject<dynamic>(requestBody);

        _logger.LogInformation($"Customer registered: {data?.name}");

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync("Customer registered successfully");

        return response;
    }
}
