using Abp.Domain.Values;
using Fiap.Domain.SeedWork.Exceptions;

namespace Fiap.Domain.GameAggregate.ValueObjects
{
    public class Genre : ValueObject
    {
        public string Name { get; }

        public Genre(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessRulesException("The genre of the game is required.");

            Name = name.Trim();
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
        }
    }
}
