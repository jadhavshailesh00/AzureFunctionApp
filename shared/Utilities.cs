using System;

namespace BankFunctionsApp.shared
{
    public static class Utilities
    {
        public static string NewId() => Guid.NewGuid().ToString("N");
    }
}
