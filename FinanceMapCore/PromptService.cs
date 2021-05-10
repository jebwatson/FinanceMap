using System;
using System.Globalization;

namespace FinanceMap
{
    public static class PromptService
    {
        public static void ProjectFromFile()
        {
            var projection = Projection.FromJson();
            
            if (projection == null)
            {
                ProjectFromCli();
                return;
            }
            
            Console.Write("Your last projection was: $");
            Console.WriteLine(projection.ProjectedAccountValue);
            Console.WriteLine();

            try
            {
                Console.Write("Please enter the date you would like to project to in the format MM/DD/YYYY: ");
                var projectionDate = Console.ReadLine();

                if (!string.IsNullOrEmpty(projectionDate))
                {
                    projection = projection with { Date = DateTime.Parse(projectionDate) };
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Bad date format, using today as the default");
            }

            var service = new AccountProjectionService();
            var projectedAccount = service
                .ForwardProjectFixedOccurenceToAccountValue(projection)
                .Projection;
            projectedAccount.ToJson();

            var result = string.Concat(
                $"Projected account value on {projection.Date.ToString(CultureInfo.CurrentCulture)}:",
                $" ${projectedAccount.ProjectedAccountValue.ToString(CultureInfo.CurrentCulture)}");

            Console.WriteLine(result);
        }

        public static void ProjectFromCli()
        {
            Account account = new() { Value = double.NaN };
            var nextPayday = DateTime.Today;
            var projectionDate = DateTime.Today;
            AccountEntry accountEntry = new() { Value = 0.00 };

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
                var projectionDateEntry = Console.ReadLine();

                if (!string.IsNullOrEmpty(payday) && !string.IsNullOrEmpty(projectionDateEntry))
                {
                    nextPayday = DateTime.Parse(payday);
                    projectionDate = DateTime.Parse(projectionDateEntry);
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
                    accountEntry = accountEntry with
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

            var service = new AccountProjectionService();
            var projection = new Projection
            {
                Account = account with
                {
                    FixedRecurringOccurence = accountEntry
                },
                NextOccurence = nextPayday,
                Date = projectionDate
            };
            var projectedAccount = service
                .ForwardProjectFixedOccurenceToAccountValue(projection)
                .Projection;

            var result = string.Concat(
                $"Projected account value on {projectionDate.ToString(CultureInfo.CurrentCulture)}:",
                $" {projectedAccount.ProjectedAccountValue.ToString(CultureInfo.CurrentCulture)}");

            Console.WriteLine(result);
        }
    }
}