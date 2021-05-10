using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinanceMap
{
    public record Projection
    {
        public Account Account { get; init; }
        public double ProjectedAccountValue { get; init; }
        public DateTime NextOccurence { get; init; }
        public DateTime Date { get; init; } = DateTime.Today;
        
        public string ToJson()
        {
            var jProjection = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(@"../../../../MyLastProjection.json", jProjection);
            return jProjection;
        }

        public static Projection FromJson()
        {
            var jProjection = File.ReadAllText(@"../../../../MyLastProjection.json");
            return JsonConvert.DeserializeObject<Projection>(jProjection);
        }
    }
}