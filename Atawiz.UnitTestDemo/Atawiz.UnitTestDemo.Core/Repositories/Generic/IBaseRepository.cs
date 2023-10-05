using System.Linq.Expressions;

namespace Atawiz.UnitTestDemo.Core.Repositories.Generic
{
    public interface IBaseRepository<T> where T : class
    {

        // Read
        Task<T?> FindByIdAsync(int id);
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);

        // Write
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        Task<T> UpdateAsync(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        Task DeleteAsync(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
