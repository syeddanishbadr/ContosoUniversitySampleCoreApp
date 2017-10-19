using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Core
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsnyc(bool noTracking = false);
        Task<T> GetByIdAsnyc(int id, bool noTracking = false);
        Task<T> FindAsync(Expression<Func<T, bool>> filter, bool noTracking = false);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> filter, bool noTracking = false);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
