using System.Collections.Generic;

namespace ConversorMoeda.Application.Entities
{
    public class CurrencyConverterResult
    {
        public bool Success { get; set; }
        public string Terms { get; set; }
        public string Privacy { get; set; }
        public int Timestamp { get; set; }
        public string Source { get; set; }
        public Dictionary<string, decimal> Quotes { get; set; } = new Dictionary<string, decimal>();
    }
}
