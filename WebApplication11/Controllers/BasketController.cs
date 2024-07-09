using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication11.Data;
using WebApplication11.Models;

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
            List<Product> products;
            if (string.IsNullOrEmpty(basket))
            {
           products = new List<Product>();
            }
            else
            {
                products =JsonConvert.DeserializeObject<List<Product>>(basket);
            }
            Product product = products.FirstOrDefault(s=>s.Id==existedProduct.Id);
            if (product == null)
            {
                products.Add(existedProduct);
            }
            else
            {

            }
            
            Response.Cookies.Append("basket",JsonConvert.SerializeObject(products));
            return Content("elave olundu");
        }
        public IActionResult ShowBasket()
        {
            string basket = Request.Cookies["basket"];
            List<Product> products;
            if (string.IsNullOrEmpty(basket))
            {
                products = new List<Product>();
            }
            else
            {
                products= JsonConvert.DeserializeObject<List<Product>>(basket);
            }
            return Json(products);
        }
        //public IActionResult GetItem()
        //{
        //  var result=  HttpContext.Session.GetString("group");

        //    return Content(result);
        //}
    }
}
