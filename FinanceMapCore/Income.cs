using System;

namespace FinanceMap
{
    public record Income
    {
        /// <summary>
        /// How much money is this income?
        /// </summary>
        public double Value { get; init; }
        
        /// <summary>
        /// How often is this income?
        /// </summary>
        public TimeSpan Frequency { get; init; }
    }
}