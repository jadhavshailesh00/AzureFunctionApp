using System;

namespace BankFunctionsApp.FunctionApp.Models
{
    public class CardTransaction
    {
        public string CardId { get; set; } = "";
        public string TransactionId { get; set; } = System.Guid.NewGuid().ToString();
        public decimal Amount { get; set; }
        public string Merchant { get; set; } = "";
        public string Currency { get; set; } = "USD";
        public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
