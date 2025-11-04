namespace BankFunctionsApp.FunctionApp.Models
{
    public class TransferRequest
    {
        public string TransferId { get; set; } = System.Guid.NewGuid().ToString();
        public string FromAccountId { get; set; } = "";
        public string ToAccountId { get; set; } = "";
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string Reference { get; set; } = "";
    }
}
