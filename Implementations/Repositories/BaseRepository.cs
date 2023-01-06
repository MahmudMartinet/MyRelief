using Microsoft.EntityFrameworkCore;
using Relief.Contexts;
using Relief.Contracts;
using Relief.Interfaces.Repositories;
using System.Linq.Expressions;

namespace Relief.Implementations.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected ApplicationContext _context;
        public async Task<T> Register(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public  Task<T> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            var ans = await _context.Set<T>().FirstOrDefaultAsync(expression);
            return ans;
        }
    }
}
