using api.Data.Interfaces;
using api.Infrastructure.Data.Repositories.Interfaces;

namespace api.Infrastructure.Data.Repositories
{
    public class BaseRepository<T, Tid> :
        IBaseRepository<T, Tid>
        where T : class, IEntityWithTypedId<Tid>
    {
        private readonly ICrudBaseRepository<T, Tid> crudBaseRepository;

        public BaseRepository(
            ICrudBaseRepository<T, Tid> crudBaseRepository)
        {
            this.crudBaseRepository = crudBaseRepository;
        }

        public IQueryable<T> Query()
        {
            return this.crudBaseRepository.Query();
        }

        public Task<List<T>> GetAllAsync()
        {
            return this.crudBaseRepository.GetAllAsync();
        }

        public Task<T> CreateAsync(T entity)
        {
            return this.crudBaseRepository.CreateAsync(entity);
        }

        public Task<T> GetAsync(Tid id)
        {
            return this.crudBaseRepository.GetAsync(id);
        }

        public async Task DeleteAsync(T entity)
        {
            await this.crudBaseRepository.DeleteAsync(entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
           return await this.crudBaseRepository.UpdateAsync(entity);
        }
    }
}
