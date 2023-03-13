using api.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Data.Repositories
{
    public class CrudBaseRepository<T> :
        ICrudBaseRepository<T>
        where T : class
    {
        private readonly ApiContext database;

        public CrudBaseRepository(ApiContext database)
        {
            this.database = database;
        }

        public IQueryable<T> Query()
        {
            return this.database.Set<T>()
                .AsQueryable();
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return this.Query()
                .ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await this.database.AddAsync(entity);
            await this.database.SaveChangesAsync();

            return entity;
        }
    }
}
