using System;

namespace FinanceMap
{
    /// <summary>
    /// Provides methods for projecting account value
    /// </summary>
    public interface IProject
    {
        Account ForwardProjectFixedIncomeToAccountValue(
            Account currentAccount,
            DateTime nextPayday,
            DateTime projectionDate,
            Income income);
    }
}