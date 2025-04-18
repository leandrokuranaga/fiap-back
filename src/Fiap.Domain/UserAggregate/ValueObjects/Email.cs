using Abp.Domain.Values;

namespace Fiap.Domain.UserAggregate.ValueObjects
{
    public class Email : ValueObject
    {
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
