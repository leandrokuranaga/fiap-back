﻿using Fiap.Domain.PromotionAggregate;
using Fiap.Infra.Data.Repositories.Base;

namespace Fiap.Infra.Data.Repositories
{
    public class PromotionRepository(Context context) : BaseRepository<Promotion>(context), IPromotionRepository
    {
    }
}
