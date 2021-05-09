using System.IO;
using Newtonsoft.Json;

namespace FinanceMap
{
    public record Account
    {
        public double Value { get; init; } = double.NaN;
        
        public AccountEntry FixedRecurringIncome { get; init; }

        // TODO: Can probably make these extension methods for records
        public string ToJson()
        {
            var jAccount = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(@"../../../../MyAccount.json", jAccount);
            return jAccount;
        }

        public static Account FromJson()
        {
            var jAccount = File.ReadAllText(@"../../../../MyAccount.json");
            return JsonConvert.DeserializeObject<Account>(jAccount);
        }
    }
}