using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Core
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        internal DataBaseContext _context;
        internal DbSet<T> _dbSet;

        public RepositoryBase(DataBaseContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> filter, bool noTracking = false)
        {
            return
                noTracking
                ? await _dbSet.AsNoTracking().SingleOrDefaultAsync(filter)
                : await _dbSet.SingleOrDefaultAsync(filter);
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> filter, bool noTracking = false)
        {
            return
               noTracking
               ? await _dbSet.Where(filter).AsNoTracking().ToListAsync()
               : await _dbSet.Where(filter).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsnyc(bool noTracking = false)
        {
            return
                noTracking
                ? await _dbSet.AsNoTracking().ToListAsync()
                : await _dbSet.ToListAsync();
        }

        //public virtual async Task Insert(T entity)
        //{
        //    await _dbSet.AddAsync(entity);
        //    //_context.Set<T>().Add(entity);
        //    //await _context.SaveChangesAsync();
        //}

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        //public virtual async Task Update(T entity)
        //{
        //    _dbSet.Attach(entity);
        //   await _context.Entry(entity).State = EntityState.Modified;
        //    //_context.Entry(entity).State = EntityState.Modified;
        //    //await _context.SaveChangesAsync();
        //}

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        //public virtual async Task Delete(T entity)
        //{
        //    _context.Set<T>().Remove(entity);
        //    await _context.SaveChangesAsync();
        //}

        public virtual async Task<T> GetByIdAsnyc(int id, bool noTracking = false)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
