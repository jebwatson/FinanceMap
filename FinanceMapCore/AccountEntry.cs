using System;

namespace FinanceMap
{
    /// <summary>
    /// A fixed, regularly recurring income.
    /// </summary>
    public record AccountEntry
    {
        /// <summary>
        /// How much money is this income?
        /// </summary>
        /// <remarks>Default is 0.00</remarks>
        public double Value { get; init; } = 0.00;

        /// <summary>
        /// How often is this income?
        /// </summary>
        /// <remarks>Default is 14 days.</remarks>
        public TimeSpan Frequency { get; init; } = TimeSpan.FromDays(14);
    }
}