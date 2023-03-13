namespace api.Infrastructure.Data.Repositories.Interfaces
{
    public interface ICrudBaseRepository<T>
        where T : class
    {
        IQueryable<T> Query();

        Task<List<T>> GetAllAsync();

        Task<T> CreateAsync(T entity);

    }
}
