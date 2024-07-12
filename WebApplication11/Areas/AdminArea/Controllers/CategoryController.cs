using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Areas.AdminArea.ViewModels.Category;
using WebApplication11.Data;
using WebApplication11.Models;

namespace WebApplication11.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly FiorelloDbContext _fiorelloDbContext;

        public CategoryController(FiorelloDbContext fiorelloDbContext)
        {
            _fiorelloDbContext = fiorelloDbContext;
        }

        public async Task<IActionResult> Index()
        {
            var  categories= await _fiorelloDbContext.categories.AsNoTracking().ToListAsync();

            return View(categories);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if(id is null ) return BadRequest();
            var existedCategory= await _fiorelloDbContext.categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if(existedCategory == null) return NotFound();  
            return View(existedCategory);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async  Task<IActionResult> Create(CategoryCreateVM category)
        {
            if(!ModelState.IsValid)  return View(category);
            var newCategory = new Category()
            {
                Name = category.Name,
                Description = category.Description,
                DateTime =DateTime.Now,
            };
            await _fiorelloDbContext.AddAsync(newCategory);
            await _fiorelloDbContext.SaveChangesAsync();
              
            return RedirectToAction(nameof(Index));
            
        }

    }
}
