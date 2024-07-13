using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
            if(await _fiorelloDbContext.categories.AnyAsync(s=>s.Name.ToLower()==category.Name.ToLower()))
            {
                 ModelState.AddModelError("Name","bele bir category movcuddur");
                return View(category);
            }
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

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return BadRequest();
            var existedCategory = await _fiorelloDbContext.categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (existedCategory == null) return NotFound();
             _fiorelloDbContext.categories.Remove(existedCategory);
          await  _fiorelloDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var existedCategory = await _fiorelloDbContext.categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (existedCategory == null) return NotFound();
            return View(new CatagoryUpdateVM() { Description=existedCategory.Description,Name=existedCategory.Name });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, CatagoryUpdateVM category)
        {
            if(!ModelState.IsValid) return BadRequest();
            if (id == null) return BadRequest();
            var existedCategory = await _fiorelloDbContext.categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existedCategory == null) return NotFound();
            var existeDCategoryNameWith = await _fiorelloDbContext.categories.AnyAsync(s => s.Name.ToLower() == category.Name.ToLower()&&s.Id!=existedCategory.Id);
            if (existeDCategoryNameWith) 
            {
                ModelState.AddModelError("Name", "bele bir category movcuddur");
                return View(category);
            }
            existedCategory.Name = category.Name;
            existedCategory.Description = category.Description;
             _fiorelloDbContext.categories.Update(existedCategory);
           await _fiorelloDbContext.SaveChangesAsync();
           return RedirectToAction(nameof(Index));  
        }
    }
}
