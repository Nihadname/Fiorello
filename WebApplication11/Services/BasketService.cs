using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication11.Data;
using WebApplication11.Services.interfaces;
using WebApplication11.ViewModels;

namespace WebApplication11.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly FiorelloDbContext fiorelloDbContext; 

        public BasketService(IHttpContextAccessor contextAccessor, FiorelloDbContext fiorelloDbContext)
        {
            _contextAccessor = contextAccessor;
            this.fiorelloDbContext = fiorelloDbContext;
        }

        public int GetBasketCount()=>GetBasketVm().Count;

        public List<BasketVM> GetBasketList()
        {
            var products = GetBasketVm();
            foreach (var BasketProduct in products)
            {
                var existedProduct = fiorelloDbContext.products.
                    Include(s => s.Images).FirstOrDefault(s => s.Id == BasketProduct.Id);
                BasketProduct.Name = existedProduct.Name;
                BasketProduct.IMageName = existedProduct.Images.FirstOrDefault(s => s.IsMain == true).Name;
                BasketProduct.Price = existedProduct.Price;
              
            }
            return products;
        }

        public decimal GetTotalPrice()=>GetBasketVm().Sum(s=>s.Price*s.BasketCount);
        private List<BasketVM> GetBasketVm()
        {
            List<BasketVM> list = new List<BasketVM>();
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            if (basket is not null)
            {
                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            return list;
        }
    }
}
