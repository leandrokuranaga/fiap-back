using Fiap.Domain.SeedWork;
using Fiap.Infra.Data;

namespace Fiap.Domain.Promotion
{
    public interface IPromotionRepository : IBaseRepository<Promotion>, IUnitOfWork
    {
    }
}
