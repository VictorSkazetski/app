using api.Infrastructure.Data.Repositories.Interfaces;

namespace api.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> :
        IBaseRepository<T>
        where T : class
    {
        private readonly ICrudBaseRepository<T> crudBaseRepository;

        public BaseRepository(ICrudBaseRepository<T> crudBaseRepository)
        {
            this.crudBaseRepository = crudBaseRepository;
        }

        public virtual IQueryable<T> Query()
        {
            return this.crudBaseRepository.Query();
        }

        public virtual Task<List<T>> GetAllAsync()
        {
            return this.crudBaseRepository.GetAllAsync();
        }

        public virtual Task<T> CreateAsync(T entity)
        {
            return this.crudBaseRepository.CreateAsync(entity);
        }
    }
}
