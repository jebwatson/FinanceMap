using System;
using FinanceMap;
using NUnit.Framework;

namespace FinanceMapTests.FinanceMapCoreTests
{
    public class ProjectAccountValueTests
    {
        private static readonly DateTime Today = DateTime.Today;
        public record ProjectAccountValueTestCase
        {
            public Account Account { get; init; }
            public DateTime NextPayday { get; init; }
            public DateTime ProjectionDate { get; init; }
            public AccountEntry AccountEntry { get; init; }
            public Account ProjectedAccount { get; init; }
        }

        private static object[] testCases =
        {
            // Project to next payday
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(14),
                ProjectionDate = Today + TimeSpan.FromDays(14),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 1000 }
            },
            // Project to next payday
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(3),
                ProjectionDate = Today + TimeSpan.FromDays(3),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 1000 }
            },
            // Project before next payday
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(14),
                ProjectionDate = Today + TimeSpan.FromDays(13),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 500 }
            },
            // Project before next payday
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(3),
                ProjectionDate = Today + TimeSpan.FromDays(2),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 500 }
            },
            // Project to next payday + 1 pay period
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(14),
                ProjectionDate = Today + TimeSpan.FromDays(28),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 1500 }
            },
            // Project to next payday + 1 pay period
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(3),
                ProjectionDate = Today + TimeSpan.FromDays(17),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 1500 }
            },
            // Project to next payday + a few days
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(14),
                ProjectionDate = Today + TimeSpan.FromDays(17),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 1000 }
            },
            // Project to next payday + a few days
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(3),
                ProjectionDate = Today + TimeSpan.FromDays(5),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 1000 }
            },
            // Project to today
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(14),
                ProjectionDate = Today,
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 500 }
            },
            // Project to today
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(3),
                ProjectionDate = Today,
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 500 }
            },
            // Project to past date
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(14),
                ProjectionDate = Today - TimeSpan.FromDays(2),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 500 }
            },
            // Project to past date
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today + TimeSpan.FromDays(3),
                ProjectionDate = Today - TimeSpan.FromDays(2),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 500 }
            },
            // Payday in the past
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today - TimeSpan.FromDays(14),
                ProjectionDate = Today + TimeSpan.FromDays(2),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 500 }
            },
            // Payday in the past
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                NextPayday = Today - TimeSpan.FromDays(3),
                ProjectionDate = Today + TimeSpan.FromDays(2),
                AccountEntry = new AccountEntry() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 500 }
            },
        };

        [TestCaseSource(nameof(testCases))]
        public void ForwardProjectsFixedIncomeToAccount(ProjectAccountValueTestCase tc)
        {
            var uut = new AccountProjectionService();
            var result = AccountProjectionService.ForwardProjectFixedIncomeToAccountValue(
                tc.Account,
                tc.NextPayday,
                tc.ProjectionDate,
                tc.AccountEntry);
            
            Assert.AreEqual(tc.ProjectedAccount, result);
        }
    }
}