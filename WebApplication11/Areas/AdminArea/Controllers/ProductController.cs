using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Controllers;
using WebApplication11.Models;
using WebApplication11.Repositories.interfaces;

namespace WebApplication11.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : BaseController<Product>
    {
        private readonly IRepository<Product> _repository;

        public ProductController(IRepository<Product> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _repository.GetAllAsync(s=>s.Images,s=>s.Category);
            return View(products);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return BadRequest("Product ID cannot be null.");

            var existedProduct = await _repository.GetByIdAsync(id.Value, s => s.Images, s => s.Category);

            if (existedProduct == null)
                return NotFound();

            return View(existedProduct);
        }

    }
}
