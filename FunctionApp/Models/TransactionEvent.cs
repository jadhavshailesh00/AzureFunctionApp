namespace BankFunctionsApp.FunctionApp.Models
{
    public class TransactionEvent
    {
        public string TransactionId { get; set; } = System.Guid.NewGuid().ToString();
        public string AccountId { get; set; } = "";
        public decimal Amount { get; set; }
        public string Location { get; set; } = "";
        public string HomeLocation { get; set; } = "";
        public string Merchant { get; set; } = "";
        public string Channel { get; set; } = ""; // card, atm, online
    }
}
