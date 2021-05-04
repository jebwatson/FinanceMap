using System;

namespace FinanceMap
{
    /// <summary>
    /// Provides methods for projecting account value
    /// </summary>
    public interface IProject
    {
        Account ProjectValueAtDate(
            Account currentAccount,
            DateTime startingDate,
            DateTime projectionDate,
            Income income);
    }
}