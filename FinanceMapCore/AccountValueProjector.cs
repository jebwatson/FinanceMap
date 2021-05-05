using System;
using System.ComponentModel;

namespace FinanceMap
{
    public class AccountValueProjector : IProject
    {
        /// <summary>
        /// Projects future fixed income to the account's value.
        /// </summary>
        /// <param name="currentAccount">The current account with today's account value.</param>
        /// <param name="nextPayday">The next day that income will be received.</param>
        /// <param name="projectionDate">The future date to which the account will be projected.</param>
        /// <param name="income">The fixed income to be applied to the account.</param>
        /// <returns>The current account with future fixed income projections applied.</returns>
        public Account ForwardProjectFixedIncomeToAccountValue(
            Account currentAccount,
            DateTime nextPayday,
            DateTime projectionDate,
            Income income)
        {
            // Use only date values
            nextPayday = nextPayday.Date;
            projectionDate = projectionDate.Date;
            var today = DateTime.Today;
            var daysUntilNextPayday = nextPayday - today;
            var daysUntilProjection = projectionDate - today;

            // If a past payday was selected, return the current account value
            if (daysUntilNextPayday.Days < 0)
            {
                // TODO: Log invalid state
                return currentAccount;
            }

            // If a past day was selected for projection, return the current account value.
            if (daysUntilProjection.Days < 0)
            {
                // TODO: Log invalid state
                return currentAccount;
            }

            // If the next payday is after the projected date, return the current account value.
            if (daysUntilNextPayday.Days > daysUntilProjection.Days)
            {
                return currentAccount;
            }

            // At this point, projection date is greater than or equal to the next payday
            // Calculate the number of pay periods elapsed to determine income accrued
            // This will be days from next payday to projection date divided by the pay period frequency
            var daysFromNextPaydayToProjection = projectionDate - nextPayday;
            var payPeriodsElapsed = (int)(daysFromNextPaydayToProjection / income.Frequency);

            // Casting to an integer will round down so we need to add one
            // This is because at least one pay period has occurred at this point
            payPeriodsElapsed++;
            
            var incomeDelta = income.Value * payPeriodsElapsed;
            return currentAccount with
            {
                Value = currentAccount.Value + incomeDelta
            };
        }
    }
}