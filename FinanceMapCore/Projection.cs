using System;
using System.IO;
using Newtonsoft.Json;

namespace FinanceMap
{
    public record Projection
    {
        public Account Account { get; init; }
        public DateTime NextPayday { get; init; }
        public DateTime Date { get; init; } = DateTime.Today;
        
        public string ToJson()
        {
            var jProjection = JsonConvert.SerializeObject(this);
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