using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CodeSchool.DataAccess
{
    public class EntityFrameworkRepository : IGenericRepository
    {
        private readonly DbContext _context;

        public EntityFrameworkRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<T> Find<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            var item = await _context.Set<T>().FirstOrDefaultAsync(predicate);
            return item;
        }

        public async Task<ICollection<T>> Where<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<ICollection<T>> GetAll<T>() where T : class
        {
            return await _context.Set<T>().ToListAsync();
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}