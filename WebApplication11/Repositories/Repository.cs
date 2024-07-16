using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication11.Data;
using WebApplication11.Repositories.interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApplication11.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly FiorelloDbContext _context;

        public Repository(FiorelloDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

       
        //public async Task<List<T>> GetAllAsync()
        //{
        //    return await _context.Set<T>().AsNoTracking().ToListAsync();
        //}

        public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> queryForAddingDataInto=_context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    queryForAddingDataInto = queryForAddingDataInto.Include(include);

                }
            }
            return await queryForAddingDataInto.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id, params Expression<Func<T, object>>[] includes)
        {
            if (id is null) return null;
            IQueryable<T> queryForAddingDataInto = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    queryForAddingDataInto= queryForAddingDataInto.Include(include);
                }
            }
            return await queryForAddingDataInto.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
       
    }

}
