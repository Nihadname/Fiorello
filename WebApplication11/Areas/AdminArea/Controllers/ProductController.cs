using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Controllers;
using WebApplication11.Models;
using WebApplication11.Repositories.interfaces;

namespace WebApplication11.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Category> _categoryRepository;

        public ProductController(IRepository<Product> repository, IRepository<Category> categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _repository.GetAllAsync(0,0,s=>s.Images,s=>s.Category);
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
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(0, 0), "Id", "Name");
            return View();
        }
    }
}
