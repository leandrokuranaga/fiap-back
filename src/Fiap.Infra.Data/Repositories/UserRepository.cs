using Fiap.Domain.UserAggregate;
using Fiap.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data.Repositories
{
    public class UserRepository(Context context) : BaseRepository<User>(context), IUserRepository
    {
        public async Task<User> GetByIdGameUserAsync(int id, bool noTracking = true)
        {
            IQueryable<User> query = dbSet
                .Include(u => u.LibraryGames)
                    .ThenInclude(lg => lg.Game);

            if (noTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
