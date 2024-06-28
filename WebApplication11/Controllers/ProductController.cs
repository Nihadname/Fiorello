using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;

namespace WebApplication11.Controllers
{
    public class ProductController : Controller
    {
        private readonly FiorelloDbContext fiorelloDbContext;
        public ProductController( FiorelloDbContext fiorelloDb)
        {
            fiorelloDbContext=fiorelloDb;
        }
        public IActionResult Index()
        {
           
            ViewBag.AllCount= fiorelloDbContext.products.Count();
            var products = fiorelloDbContext.products
                                             .Include(p => p.Category)
                                             .Include(p => p.Images)
                                             .AsNoTracking()
                                             .OrderByDescending(b => b.Id)
                                             .Take(4)
                                             .ToList();
            if (products.Any())
            {
                return View(products);
            }
            return View();
        }
        public async Task<IActionResult> LoadMore(int skip = 4)
        {
            var skippedProducts = await fiorelloDbContext.products.Include(p => p.Category)
                                             .Include(p => p.Images).AsNoTracking()
                                             .OrderByDescending(b => b.Id)
                                             .Skip(skip)
                                             .Take(4)
                                             .ToListAsync();
            ViewBag.AllCount = skippedProducts.Count();
            return PartialView("_ProductPartialView", skippedProducts);
        
        }

    }
}
