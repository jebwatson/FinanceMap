using System;
using System.ComponentModel;

namespace FinanceMap
{
    public class AccountValueProjector : IProject
    {
        /// <summary>
        /// Given a current <see cref="Account"/>, a future date, and an <see cref="Income"/>, project the account's value to the future date.
        /// </summary>
        /// <param name="currentAccount">The current account.</param>
        /// <param name="futureDate">The date to which the account will be projected.</param>
        /// <param name="income">The income to be applied to the account.</param>
        /// <returns>An account object projected to the future date.</returns>
        public Account ProjectValueAtDate(Account currentAccount, DateTime futureDate, Income income)
        {
            var time = (int)((futureDate - DateTime.Today) / income.Frequency);
            var value = income.Value * time;
            return currentAccount with
            {
                Value = currentAccount.Value + value
            };
        }
    }
}