using Newtonsoft.Json;
using WebApplication11.Services.interfaces;
using WebApplication11.ViewModels;

namespace WebApplication11.Services
{
    public class BasketService : IBasketService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public BasketService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public int GetBasketCount()
        {
            List<BasketVM> list=new List<BasketVM>();
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            if(basket is not null)
            {
                list= JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
          return  list.Count();
        }

        public List<BasketVM> GetBasketList()
        {
            throw new NotImplementedException();
        }

        public decimal GetTotalPrice()
        {
            List<BasketVM> list = new List<BasketVM>();
            string basket = _contextAccessor.HttpContext.Request.Cookies["basket"];
            if (basket is not null)
            {
                list = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }
            return list.Sum(s=>s.Price*s.BasketCount);
        }
    }
}
