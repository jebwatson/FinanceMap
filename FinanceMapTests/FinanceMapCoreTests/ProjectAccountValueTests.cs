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
            public DateTime StartingDate { get; init; }
            public DateTime ProjectionDate { get; init; }
            public Income Income { get; init; }
            public Account ProjectedAccount { get; init; }
        }

        private static object[] testCases =
        {
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                StartingDate = Today,
                ProjectionDate = Today + TimeSpan.FromDays(14),
                Income = new Income() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 1000 }
            },
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                StartingDate = Today,
                ProjectionDate = Today + TimeSpan.FromDays(-14),
                Income = new Income() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 0 }
            },
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                StartingDate = Today,
                ProjectionDate = Today + TimeSpan.FromDays(-28),
                Income = new Income() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = -500 }
            },
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                StartingDate = Today,
                ProjectionDate = Today + TimeSpan.FromDays(28),
                Income = new Income() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 1500 }
            },
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                StartingDate = Today,
                ProjectionDate = Today + TimeSpan.FromDays(12),
                Income = new Income() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 500 }
            },
            new ProjectAccountValueTestCase
            {
                Account = new Account() { Value = 500 },
                StartingDate = Today,
                ProjectionDate = Today + TimeSpan.FromDays(15),
                Income = new Income() { Value = 500, Frequency = TimeSpan.FromDays(14) },
                ProjectedAccount = new Account() { Value = 1000 }
            },
        };

        [TestCaseSource(nameof(testCases))]
        public void ProjectsValueAtDate(ProjectAccountValueTestCase tc)
        {
            var uut = new AccountValueProjector();
            var result = uut.ProjectValueAtDate(
                tc.Account,
                tc.StartingDate,
                tc.ProjectionDate,
                tc.Income);
            
            Assert.AreEqual(tc.ProjectedAccount, result);
        }
    }
}