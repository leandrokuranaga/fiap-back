using Fiap.Domain.PromotionAggregate;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class PromotionRepository(IUnitOfWork unitOfWork) : BaseRepository<PromotionDomain>(unitOfWork), IPromotionRepository
    {
    }
}
