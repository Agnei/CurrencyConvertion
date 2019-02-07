using System;
using System.Collections.Generic;
using System.Text;

namespace ConversorMoeda.Application.Entities
{
    public class ConverterMoedaModelView
    {
        public ConverterMoedaModelView(string from, string to, decimal quote, decimal amount)
        {
            From = from;
            To = to;
            Quote = quote;
            Amount = amount;
        }

        public string From { get; private set; }
        public string To { get; private set; }
        public decimal Quote { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Result { get => Quote * Amount; }
    }
}
