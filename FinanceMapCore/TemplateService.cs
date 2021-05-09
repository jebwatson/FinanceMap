using System;
using System.IO;
using Newtonsoft.Json;

namespace FinanceMap
{
    public class TemplateService
    {
        public static void CreateProjectionJsonTemplate()
        {
            var account = new Account
            {
                Value = 500,
                FixedRecurringIncome = new AccountEntry
                {
                    Value = 500,
                    Frequency = TimeSpan.FromDays(14)
                }
            };
            
            var projection = new Projection
            {
                Account = account,
                ProjectedAccountValue = account.Value,
                NextPayday = DateTime.Today,
                Date = DateTime.Today
            };

            var jProjection = projection.ToJson();
            Console.WriteLine("Your serialized projection:");
            Console.WriteLine(jProjection);

            Console.WriteLine("Do you want to save this to a file? (Y/N)");
            var answer = Console.ReadLine();
            var shouldWrite = answer == "Y";

            if (!shouldWrite) return;
            
            File.WriteAllText(@"../../../../MyLastProjection.json", jProjection);
            Console.WriteLine("Saved to MyLastProjection.json in solution root");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
        
        public static void CreateAccountJsonTemplate()
        {
            var account = new Account
            {
                Value = 500,
                FixedRecurringIncome = new AccountEntry
                {
                    Value = 500,
                    Frequency = TimeSpan.FromDays(14)
                }
            };
            var jAccount = account.ToJson();
            Console.WriteLine("Your serialized account:");
            Console.WriteLine(jAccount);

            Console.WriteLine("Do you want to save this to a file? (Y/N)");
            var answer = Console.ReadLine();
            var shouldWrite = answer == "Y";

            if (!shouldWrite) return;
            
            File.WriteAllText(@"../../../../MyAccount.json", jAccount);
            Console.WriteLine("Saved to MyAccount.json in solution root");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}