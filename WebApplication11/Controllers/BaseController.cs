using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;
using WebApplication11.Repositories.interfaces;

namespace WebApplication11.Controllers
{
    public abstract class BaseController<T> : Controller where T : class
    {
        protected readonly IRepository<T> _repository;
        protected readonly FiorelloDbContext _context;
        public BaseController(IRepository<T> repository)
        {
            _repository = repository;
        }

        protected async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        protected async Task<T> GetByIdAsync(int? id)
        {
            return await _repository.GetByIdAsync(id);
        }

        
        protected async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        protected async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        protected async Task DeleteAsync(T entity)
        {
            await _repository.DeleteAsync(entity);
        }
    }

}
