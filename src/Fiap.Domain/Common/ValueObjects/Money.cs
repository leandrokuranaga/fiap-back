using Abp.Domain.Values;
using Fiap.Domain.SeedWork.Exceptions;

namespace Fiap.Domain.Common.ValueObjects
{
    public class Money : ValueObject
    {
        public double Value { get; }
        public string Currency { get; }

        private static readonly HashSet<string> ValidCurrencies = new()
        {
            "USD", "EUR", "BRL", "JPY", "GBP" 
        };
        public Money(double value, string currency)
        {
            if (double.IsNaN(value))
                throw new BusinessRulesException("The price cannot be NaN.");

            if (value < 0)
                throw new BusinessRulesException("The price must be greater than or equal to 0.");

            if (string.IsNullOrWhiteSpace(currency))
                throw new BusinessRulesException("Currency is required.");

            if (!ValidCurrencies.Contains(currency.ToUpperInvariant()))
                throw new BusinessRulesException($"Invalid currency: {currency}. Supported currencies are: {string.Join(", ", ValidCurrencies)}");

            Value = value;
            Currency = currency.ToUpperInvariant();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
            yield return Currency;
        }
    }
}
