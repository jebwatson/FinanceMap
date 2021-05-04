using System;

namespace FinanceMap
{
    /// <summary>
    /// Provides methods for projecting account
    /// </summary>
    public interface IProject
    {
        Account ProjectValueAtDate(Account currentAccount, DateTime futureDate, Income income);
    }
}