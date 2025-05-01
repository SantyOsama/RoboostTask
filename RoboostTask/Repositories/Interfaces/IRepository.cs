using System.Linq.Expressions;

namespace RoboostTask.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveChangesAsyc();

    }
}
