using System;

namespace FinanceMap
{
    public class AccountProjectionService
    {
        /// <summary>
        /// Projects future fixed income to the account's value.
        /// </summary>
        /// <param name="projection">The projection to use.</param>
        /// <returns>A projection containing the current account with future fixed income projections applied.</returns>
        public static Projection ForwardProjectFixedIncomeToAccountValue(Projection projection)
        {
            // Use only date values
            projection = projection with
            {
                NextPayday = projection.NextPayday.Date,
                Date = projection.Date.Date
            };
            
            var today = DateTime.Today;
            var daysUntilNextPayday = projection.NextPayday - today;
            var daysUntilProjection = projection.Date - today;

            // If a past payday was selected, return the current account value
            if (daysUntilNextPayday.Days < 0)
            {
                // TODO: Log invalid state
                return projection;
            }

            // If a past day was selected for projection, return the current account value.
            if (daysUntilProjection.Days < 0)
            {
                // TODO: Log invalid state
                return projection;
            }

            // If the next payday is after the projected date, return the current account value.
            if (daysUntilNextPayday.Days > daysUntilProjection.Days)
            {
                return projection;
            }

            // At this point, projection date is greater than or equal to the next payday
            // Calculate the number of pay periods elapsed to determine income accrued
            // This will be days from next payday to projection date divided by the pay period frequency
            var daysFromNextPaydayToProjection = projection.Date - projection.NextPayday;
            var payPeriodsElapsed = (int)(daysFromNextPaydayToProjection / projection.Account.FixedRecurringIncome.Frequency);

            // Casting to an integer will round down so we need to add one
            // This is because at least one pay period has occurred at this point
            payPeriodsElapsed++;
            
            var incomeDelta = projection.Account.FixedRecurringIncome.Value * payPeriodsElapsed;
            return projection with
            {
                Account = projection.Account with
                {
                    Value = projection.Account.Value + incomeDelta,
                }
            };
        }
        
        /// <summary>
        /// Projects future fixed income to the account's value.
        /// </summary>
        /// <param name="currentAccount">The current account with today's account value.</param>
        /// <param name="nextPayday">The next day that income will be received.</param>
        /// <param name="projectionDate">The future date to which the account will be projected.</param>
        /// <param name="fixedRecurringIncome">The fixed income to be applied to the account.</param>
        /// <returns>A projection containing the current account with future fixed income projections applied.</returns>
        [Obsolete("Use the ForwardProjectFixedIncomeToAccountValue with Projection parameter")]
        public static Projection ForwardProjectFixedIncomeToAccountValue(
            Account currentAccount,
            DateTime nextPayday,
            DateTime projectionDate,
            AccountEntry fixedRecurringIncome)
        {
            // Use only date values
            nextPayday = nextPayday.Date;
            projectionDate = projectionDate.Date;
            
            var projection = new Projection
            {
                Account = currentAccount,
                NextPayday = nextPayday,
                Date = projectionDate
            };
            
            var today = DateTime.Today;
            var daysUntilNextPayday = nextPayday - today;
            var daysUntilProjection = projectionDate - today;

            // If a past payday was selected, return the current account value
            if (daysUntilNextPayday.Days < 0)
            {
                // TODO: Log invalid state
                return projection;
            }

            // If a past day was selected for projection, return the current account value.
            if (daysUntilProjection.Days < 0)
            {
                // TODO: Log invalid state
                return projection;
            }

            // If the next payday is after the projected date, return the current account value.
            if (daysUntilNextPayday.Days > daysUntilProjection.Days)
            {
                return projection;
            }

            // At this point, projection date is greater than or equal to the next payday
            // Calculate the number of pay periods elapsed to determine income accrued
            // This will be days from next payday to projection date divided by the pay period frequency
            var daysFromNextPaydayToProjection = projectionDate - nextPayday;
            var payPeriodsElapsed = (int)(daysFromNextPaydayToProjection / fixedRecurringIncome.Frequency);

            // Casting to an integer will round down so we need to add one
            // This is because at least one pay period has occurred at this point
            payPeriodsElapsed++;
            
            var incomeDelta = fixedRecurringIncome.Value * payPeriodsElapsed;
            return projection with
            {
                Account = projection.Account with
                {
                    Value = currentAccount.Value + incomeDelta
                }
            };
        }
    }
}