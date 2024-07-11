using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;

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
    }
}
