using Abp.Domain.Values;
using Abp.Extensions;
using Fiap.Domain.SeedWork.Exceptions;

namespace Fiap.Domain.GameAggregate.ValueObjects
{
    public class Price : ValueObject
    {
        public double Value { get; }

        public Price(double value)
        {
            if (double.IsNaN(value))
                throw new BusinessRulesException("The price of the game cannot be NaN.");

            if (value < 0)
                throw new BusinessRulesException("The price of the game must be greater than or equal to 0.");


            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
