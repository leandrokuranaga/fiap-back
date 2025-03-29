using Microsoft.EntityFrameworkCore;

namespace Fiap.Infra.Data
{
    public class UnitOfWork(Context context) : IUnitOfWork
    {
        public Context Context { get; set; } = context;

        public async Task CommitAsync()
        {
            using (var transaction = await this.Context.Database.BeginTransactionAsync())
            {
                try
                {
                    await this.Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task CommitWithIdentityInsertAsync(string table)
        {
            using var transaction = await this.Context.Database.BeginTransactionAsync();
            try
            {
                await Context.Database.ExecuteSqlRawAsync($"ALTER SEQUENCE {table}_id_seq RESTART WITH 1;");
                await this.Context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
