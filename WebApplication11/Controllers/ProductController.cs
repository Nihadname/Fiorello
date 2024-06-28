using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
                                             .Take(4)
                                             .ToList();
            ViewBag.AllCount = fiorelloDbContext.products.Count();
           
            if (products.Any())
            {
                return View(products);
            }
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if(id == null) return BadRequest();
            var product = fiorelloDbContext.products.AsNoTracking().FirstOrDefault(s=>s.Id==id);
            if (product == null) return NotFound();
            return View(product);
        }
        public  IActionResult LoadMore(int skip = 4)
        {
            try
            {
                var skippedProducts = fiorelloDbContext.products
                                                              .Include(p => p.Category)
                                                              .Include(p => p.Images)
                                                              .AsNoTracking()
                                                              .Skip(skip)
                                                              .Take(4)
                                                              .ToList();

                if (skippedProducts == null)
                {
                   
                    Debug.WriteLine("Skipped products list is null.");
                }
                else
                {
                    Debug.WriteLine($"Skipped products count: {skippedProducts.Count()}");
                }

                ViewBag.ProductCount =  fiorelloDbContext.products.Count();
                return PartialView("_ProductPartialView", skippedProducts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }

        }

    }
}
