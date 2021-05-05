using System;
using System.Globalization;

namespace FinanceMap
{
    public static class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello, welcome to Finance Map!");
            Account account = new() { Value = double.NaN };
            var nextPayday = DateTime.Today;
            var projectionDate = DateTime.Now;
            Income income = new() { Value = 0.00 };
            IProject projectionService = new AccountValueProjector();

            while (double.IsNaN(account.Value))
            {
                Console.Write("Please enter your current account value: ");
                var accountValue = Console.ReadLine();
                if (string.IsNullOrEmpty(accountValue)) continue;

                try
                {
                    var value = Convert.ToDouble(accountValue);
                    account = account with { Value = value };
                }
                catch (FormatException)
                {
                    Console.WriteLine("Account value is formatted incorrectly.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Account value is too large to process.");
                }
            }

            try
            {
                Console.Write("Please enter your next payday in the format MM/DD/YYY: ");
                var payday = Console.ReadLine();
                
                Console.Write("Please enter the date you would like to project to in the format MM/DD/YYYY: ");
                var projection = Console.ReadLine();

                if (!string.IsNullOrEmpty(payday) && !string.IsNullOrEmpty(projection))
                {
                    nextPayday = DateTime.Parse(payday);
                    projectionDate = DateTime.Parse(projection);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Bad date format, using today as the default");
            }

            try
            {
                Console.Write("Please enter your income starting with its value per pay period: ");
                var incomeValue = Console.ReadLine();

                Console.Write("Now enter how often the income occurs in days: ");
                var incomeFrequency = Console.ReadLine();

                if (!string.IsNullOrEmpty(incomeValue) && !string.IsNullOrEmpty(incomeFrequency))
                {
                    income = income with
                    {
                        Value = Convert.ToDouble(incomeValue),
                        Frequency = TimeSpan.FromDays(Convert.ToInt16(incomeFrequency))
                    };
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Bad format, using default income values");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Value too large to process, using default income values");
            }

            var projectedAccount = projectionService
                .ForwardProjectFixedIncomeToAccountValue(account, nextPayday, projectionDate, income);

            var result = string.Concat(
                $"Projected account value on {projectionDate.ToString(CultureInfo.CurrentCulture)}:",
                $" {projectedAccount.Value.ToString(CultureInfo.CurrentCulture)}");
            
            Console.WriteLine(result);
        }
    }
}
