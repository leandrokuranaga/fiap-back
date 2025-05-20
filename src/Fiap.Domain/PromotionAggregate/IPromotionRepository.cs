using Fiap.Domain.SeedWork;

namespace Fiap.Domain.PromotionAggregate
{
    public interface IPromotionRepository : IBaseRepository<Promotion>, IUnitOfWork
    {
    }
}
