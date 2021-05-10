using System;
using FinanceMap;
using NUnit.Framework;

namespace FinanceMapTests.FinanceMapCoreTests
{
    public class ProjectAccountValueTests
    {
        private static readonly DateTime Today = DateTime.Today;

        private static object[] testCases =
        {
            // Project to next payday
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 1000,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today + TimeSpan.FromDays(14)
                }
            },
            // Project to next payday
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 1000,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today + TimeSpan.FromDays(3)
                }
            },
            // Project before next payday
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today + TimeSpan.FromDays(13)
                }
            },
            // Project before next payday
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today + TimeSpan.FromDays(2)
                }
            },
            // Project to next payday + 1 pay period
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 1500,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today + TimeSpan.FromDays(28)
                }
            },
            // Project to next payday + 1 pay period
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 1500,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today + TimeSpan.FromDays(17)
                }
            },
            // Project to next payday + a few days
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 1000,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today + TimeSpan.FromDays(17)
                }
            },
            // Project to next payday + a few days
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 1000,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today + TimeSpan.FromDays(5)
                }
            },
            // Project to today
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today
                }
            },
            // Project to today
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today
                }
            },
            // Project to past date
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today - TimeSpan.FromDays(2)
                }
            },
            // Project to past date
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today - TimeSpan.FromDays(2)
                }
            },
            // Payday in the past
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today - TimeSpan.FromDays(14),
                    Date = Today + TimeSpan.FromDays(2)
                }
            },
            // Payday in the past
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = 500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today - TimeSpan.FromDays(3),
                    Date = Today + TimeSpan.FromDays(2)
                }
            },
            // Project to next expense
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 0,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today + TimeSpan.FromDays(14)
                }
            },
            // Project to next expense
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 0,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today + TimeSpan.FromDays(3)
                }
            },
            // Project before next expense
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today + TimeSpan.FromDays(13)
                }
            },
            // Project before next expense
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today + TimeSpan.FromDays(2)
                }
            },
            // Project to next expense + 1 expense period
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = -500,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today + TimeSpan.FromDays(28)
                }
            },
            // Project to next expense + 1 expense period
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = -500,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today + TimeSpan.FromDays(17)
                }
            },
            // Project to next expense + a few days
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 0,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today + TimeSpan.FromDays(17)
                }
            },
            // Project to next expense + a few days
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 0,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today + TimeSpan.FromDays(5)
                }
            },
            // Project to today
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today
                }
            },
            // Project to today
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today
                }
            },
            // Project to past date
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(14),
                    Date = Today - TimeSpan.FromDays(2)
                }
            },
            // Project to past date
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today + TimeSpan.FromDays(3),
                    Date = Today - TimeSpan.FromDays(2)
                }
            },
            // expense in the past
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today - TimeSpan.FromDays(14),
                    Date = Today + TimeSpan.FromDays(2)
                }
            },
            // expense in the past
            new ProjectAccountValueTestCase
            {
                Projection = new Projection
                {
                    Account = new Account
                    {
                        Value = 500,
                        FixedRecurringOccurence = new AccountEntry
                        {
                            Value = -500,
                            Frequency = TimeSpan.FromDays(14)
                        }
                    },
                    ProjectedAccountValue = 500,
                    NextOccurence = Today - TimeSpan.FromDays(3),
                    Date = Today + TimeSpan.FromDays(2)
                }
            }
        };

        [TestCaseSource(nameof(testCases))]
        public void ForwardProjectsFixedIncomeToAccount(ProjectAccountValueTestCase tc)
        {
            const double delta = 0.0000001;
            var uut = new AccountProjectionService();
            var result = uut.ForwardProjectFixedIncomeToAccountValue(
                tc.Projection.Account,
                tc.Projection.NextOccurence,
                tc.Projection.Date,
                tc.Projection.Account.FixedRecurringOccurence);

            Assert.AreEqual(
                tc.Projection.ProjectedAccountValue,
                result.Projection.ProjectedAccountValue,
                delta);

            result = uut.ForwardProjectFixedIncomeToAccountValue(tc.Projection);
            
            Assert.AreEqual(
                tc.Projection.ProjectedAccountValue,
                result.Projection.ProjectedAccountValue,
                delta);
        }

        [TestCaseSource(nameof(testCases))]
        public void ForwardProjectsFixedOccurenceToAccount(ProjectAccountValueTestCase tc)
        {
            const double delta = 0.0000001;
            var uut = new AccountProjectionService();
            var result = uut.ForwardProjectFixedIncomeToAccountValue(tc.Projection);
            
            Assert.AreEqual(
                tc.Projection.ProjectedAccountValue,
                result.Projection.ProjectedAccountValue,
                delta);
        }

        public record ProjectAccountValueTestCase
        {
            public Projection Projection { get; init; }
        }
    }
}