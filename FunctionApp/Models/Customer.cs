using System;

namespace BankFunctionsApp.FunctionApp.Models
{
    public class Customer
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string AccountId { get; set; } = "";
    }
}
