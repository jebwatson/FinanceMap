using System;

namespace FinanceMap
{
    /// <summary>
    /// Provides methods for projecting account value
    /// </summary>
    public interface IProject
    {
        Account ForwardProjectWithFixedIncome(
            Account currentAccount,
            DateTime nextPayday,
            DateTime projectionDate,
            Income income);
    }
}