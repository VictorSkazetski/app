namespace api.Infrastructure.Data.Repositories.Interfaces
{
    public interface IBaseRepository<T, Tid>
        where T : class
    {
        IQueryable<T> Query();

        Task<List<T>> GetAllAsync();

        Task<T> CreateAsync(T entity);

        Task<T> GetAsync(Tid id);

        Task DeleteAsync(T entity);

        Task<T> UpdateAsync(T entity);
    }
}
