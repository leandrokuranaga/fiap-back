using Abp.Domain.Values;

namespace Fiap.Domain.Common.ValueObjects
{
    public class Money : ValueObject
    {
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
