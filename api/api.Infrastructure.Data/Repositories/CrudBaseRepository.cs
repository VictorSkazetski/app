using api.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Data.Repositories
{
    public class CrudBaseRepository<T, Tid> :
        ICrudBaseRepository<T, Tid>
        where T : class
    {
        private readonly ApiContext database;

        public CrudBaseRepository(ApiContext database)
        {
            this.database = database;
        }

        public async Task<T> GetAsync(Tid id)
        {
            return await this.database.Set<T>()
                .FindAsync(id);
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

        public async Task DeleteAsync(T entity)
        {
            this.database.Set<T>()
                .Remove(entity);
            await this.database.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this.database.Update(entity);
            await this.database.SaveChangesAsync();

            return entity;
        }
    }
}
