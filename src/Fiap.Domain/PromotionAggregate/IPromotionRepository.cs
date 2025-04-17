using Fiap.Domain.SeedWork;
using Fiap.Infra.Data;

namespace Fiap.Domain.PromotionAggregate
{
    public interface IPromotionRepository : IBaseRepository<PromotionDomain>, IUnitOfWork
    {
    }
}
