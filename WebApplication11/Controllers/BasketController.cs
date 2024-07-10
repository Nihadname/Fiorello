using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication11.Data;
using WebApplication11.Data.Migrations;
using WebApplication11.Models;
using WebApplication11.ViewModels;
 
namespace WebApplication11.Controllers
{
    public class BasketController : Controller
    {
        private readonly FiorelloDbContext fiorelloDbContext;

        public BasketController(FiorelloDbContext fiorelloDb)
        {
            fiorelloDbContext = fiorelloDb;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddItem(int? id)
        {
            if (id is null) return BadRequest();
            var existedProduct=fiorelloDbContext.products.FirstOrDefault(s=>s.Id==id);
            if (existedProduct == null) return NotFound();
            string basket = Request.Cookies["basket"];
            List<BasketVM> products;
            if (string.IsNullOrEmpty(basket))
            {
           products = new List<BasketVM>();
            }
            else
            {
                products =JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            BasketVM product = products.FirstOrDefault(s=>s.Id==existedProduct.Id);
            if (product == null)
            {
                products.Add(new BasketVM()
                {
                    Id = existedProduct.Id,
                    BasketCount = 1,
                    Price=existedProduct.Price,
                    //Name=existedProduct.Name,
                });
            }
            else
            {
                product.BasketCount++;
            }
            
            Response.Cookies.Append("basket",JsonConvert.SerializeObject(products));
            return RedirectToAction(nameof(ShowBasket));   
        }
        public IActionResult ShowBasket()
        {
            string basket = Request.Cookies["basket"];
            List<BasketVM> products;
            if (string.IsNullOrEmpty(basket))
            {
                products = new List<BasketVM>();
            }
            else
            {
                products= JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach(var BasketProduct in products)
                {
                    var existedProduct=fiorelloDbContext.products.
                        Include(s=>s.Images).FirstOrDefault(s=>s.Id== BasketProduct.Id);
                    BasketProduct.Name = existedProduct.Name;
                    BasketProduct.IMageName = existedProduct.Images.FirstOrDefault(s => s.IsMain == true).Name;
          BasketProduct.Price=existedProduct.Price;
                    decimal total = products.Sum(item => item.Price * item.BasketCount);
                    ViewBag.BasketPrice = total;
                }
            }
            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAmount(int id, int amount)
        {
            string basket = Request.Cookies["basket"];
            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            BasketVM existedProduct=products.FirstOrDefault(s=>s.Id==id);   
            if(existedProduct==null) return NotFound();
            existedProduct.BasketCount += amount;
            if (existedProduct.BasketCount < 1) existedProduct.BasketCount = 1;
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));

            return Json(new { success = true} );

        }
      
        public async Task<IActionResult> DeleteItem(int id)
        {
            string basket = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basket)) return BadRequest("Basket is empty.");

            var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            if (products == null) return BadRequest("Basket is empty.");

            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            products.Remove(product);

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            return RedirectToAction("Index","home");   
        }
        //public IActionResult GetItem()
        //{
        //  var result=  HttpContext.Session.GetString("group");

        //    return Content(result);
        //}
    }
}
