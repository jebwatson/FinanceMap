using System;

namespace FinanceMap
{
    public class AccountProjectionService
    {
        public Projection Projection { get; private set; }

        /// <summary>
        ///     Projects future fixed expense to the account's value.
        /// </summary>
        /// <param name="projection">The projection to use.</param>
        /// <returns>The service.</returns>
        public AccountProjectionService ForwardProjectFixedOccurenceToAccountValue(Projection projection)
        {
            // Use only date values
            this.Projection = projection with
            {
                NextOccurence = projection.NextOccurence.Date,
                Date = projection.Date.Date
            };

            var today = DateTime.Today;
            var daysUntilNextOccurence = this.Projection.NextOccurence - today;
            var daysUntilProjection = this.Projection.Date - today;

            // If a past occurence was selected, return the current account value
            if (daysUntilNextOccurence.Days < 0)
            {
                // TODO: Log invalid state
                return this;
            }

            // If a past day was selected for projection, return the current account value.
            if (daysUntilProjection.Days < 0)
            {
                // TODO: Log invalid state
                return this;
            }

            // If the next occurence is after the projected date, return the current account value.
            if (daysUntilNextOccurence.Days > daysUntilProjection.Days)
            {
                return this;
            }

            // At this point, projection date is greater than or equal to the next occurence
            // Calculate the number of occurence periods elapsed to determine net change
            // This will be days from next occurence to projection date divided by the occurence period frequency
            var daysFromNextOccurenceToProjection = this.Projection.Date - this.Projection.NextOccurence;
            var occurencePeriodsElapsed =
                (int) (daysFromNextOccurenceToProjection / this.Projection.Account.FixedRecurringOccurence.Frequency);

            // Casting to an integer will round down so we need to add one
            // This is because at least one pay period has occurred at this point
            occurencePeriodsElapsed++;

            var delta = this.Projection.Account.FixedRecurringOccurence.Value * occurencePeriodsElapsed;
            this.Projection = this.Projection with
            {
                ProjectedAccountValue = this.Projection.Account.Value + delta
            };
            return this;
        }

        /// <summary>
        ///     Projects future fixed income to the account's value.
        /// </summary>
        /// <param name="projection">The projection to use.</param>
        /// <returns>The service.</returns>
        [Obsolete("Use the ForwardProjectFixedOccurence method instead")]
        public AccountProjectionService ForwardProjectFixedIncomeToAccountValue(Projection projection)
        {
            // Use only date values
            this.Projection = projection with
            {
                NextOccurence = projection.NextOccurence.Date,
                Date = projection.Date.Date
            };

            var today = DateTime.Today;
            var daysUntilNextPayday = this.Projection.NextOccurence - today;
            var daysUntilProjection = this.Projection.Date - today;

            // If a past payday was selected, return the current account value
            if (daysUntilNextPayday.Days < 0)
            {
                // TODO: Log invalid state
                return this;
            }

            // If a past day was selected for projection, return the current account value.
            if (daysUntilProjection.Days < 0)
            {
                // TODO: Log invalid state
                return this;
            }

            // If the next payday is after the projected date, return the current account value.
            if (daysUntilNextPayday.Days > daysUntilProjection.Days)
            {
                return this;
            }

            // At this point, projection date is greater than or equal to the next payday
            // Calculate the number of pay periods elapsed to determine income accrued
            // This will be days from next payday to projection date divided by the pay period frequency
            var daysFromNextPaydayToProjection = this.Projection.Date - this.Projection.NextOccurence;
            var payPeriodsElapsed =
                (int) (daysFromNextPaydayToProjection / this.Projection.Account.FixedRecurringOccurence.Frequency);

            // Casting to an integer will round down so we need to add one
            // This is because at least one pay period has occurred at this point
            payPeriodsElapsed++;

            var incomeDelta = this.Projection.Account.FixedRecurringOccurence.Value * payPeriodsElapsed;
            this.Projection = this.Projection with
            {
                ProjectedAccountValue = this.Projection.Account.Value + incomeDelta
            };
            return this;
        }

        /// <summary>
        ///     Projects future fixed income to the account's value.
        /// </summary>
        /// <param name="currentAccount">The current account with today's account value.</param>
        /// <param name="nextPayday">The next day that income will be received.</param>
        /// <param name="projectionDate">The future date to which the account will be projected.</param>
        /// <param name="fixedRecurringIncome">The fixed income to be applied to the account.</param>
        /// <returns>A projection containing the current account with future fixed income projections applied.</returns>
        [Obsolete("Use the ForwardProjectFixedIncomeToAccountValue with Projection parameter")]
        public AccountProjectionService ForwardProjectFixedIncomeToAccountValue(
            Account currentAccount,
            DateTime nextPayday,
            DateTime projectionDate,
            AccountEntry fixedRecurringIncome)
        {
            // Use only date values
            nextPayday = nextPayday.Date;
            projectionDate = projectionDate.Date;

            this.Projection = new Projection
            {
                Account = currentAccount,
                ProjectedAccountValue = currentAccount.Value,
                NextOccurence = nextPayday,
                Date = projectionDate
            };

            var today = DateTime.Today;
            var daysUntilNextPayday = nextPayday - today;
            var daysUntilProjection = projectionDate - today;

            // If a past payday was selected, return the current account value
            if (daysUntilNextPayday.Days < 0)
            {
                // TODO: Log invalid state
                return this;
            }

            // If a past day was selected for projection, return the current account value.
            if (daysUntilProjection.Days < 0)
            {
                // TODO: Log invalid state
                return this;
            }

            // If the next payday is after the projected date, return the current account value.
            if (daysUntilNextPayday.Days > daysUntilProjection.Days)
            {
                return this;
            }

            // At this point, projection date is greater than or equal to the next payday
            // Calculate the number of pay periods elapsed to determine income accrued
            // This will be days from next payday to projection date divided by the pay period frequency
            var daysFromNextPaydayToProjection = projectionDate - nextPayday;
            var payPeriodsElapsed = (int) (daysFromNextPaydayToProjection / fixedRecurringIncome.Frequency);

            // Casting to an integer will round down so we need to add one
            // This is because at least one pay period has occurred at this point
            payPeriodsElapsed++;

            var incomeDelta = fixedRecurringIncome.Value * payPeriodsElapsed;
            this.Projection = this.Projection with
            {
                ProjectedAccountValue = this.Projection.Account.Value + incomeDelta
            };

            return this;
        }
    }
}