using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication11.Data;
using WebApplication11.ViewModels;

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
                                             .Take(4)
                                             .Select(p => new ProductVM { Name=p.Name,
                                                 Price=p.Price, 
                                                 categoryName=p.Category.Name,
                                                 MainIMage=p.Images.FirstOrDefault(s=>s.IsMain==true).Name})
                                             .ToList();
            var products2 = fiorelloDbContext.products
                                           .Include(p => p.Category)
                                           .Include(p => p.Images)
                                           .Take(4).ToList();
            ViewBag.AllCount = fiorelloDbContext.products.Count();
            //List<ProductVM> productList = new();
            //foreach (var product in products)
            //{
            //    ProductVM productVM = new ProductVM();
            //    productVM.categoryName = product.Category.Name;
            //    productVM.Price = product.Price;
            //    productVM.Name = product.Name;
            //    productVM.MainIMage = product.Images.FirstOrDefault(s=>s.IsMain==true).Name;
            //    productList.Add(productVM);
            //}
            if (products.Any())
            {
                return View(products2);
            }
            return View();
        }
        public IActionResult Detail(int? id)
        {
            if(id == null) return BadRequest();
            var product = fiorelloDbContext.products.Include(p => p.Category)
                                             .Include(p => p.Images)
                                             .AsNoTracking().FirstOrDefault(s=>s.Id==id);
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
