using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication11.Data;
using WebApplication11.ViewModels;

namespace WebApplication11.ViewCompenents
{
    public class SettingHeaderViewComponent : ViewComponent
    {
        private readonly FiorelloDbContext _context;
        public SettingHeaderViewComponent(FiorelloDbContext context)
        {
            _context = context;
            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //string basket = Request.Cookies["basket"];
            //if (!string.IsNullOrEmpty(basket))
            //{
            //    var products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            //    ViewBag.TotalPrice=products.Sum(x => x.Price*x.BasketCount);
            //    ViewBag.ProductCount=products.Count();
            //}

                var settings =_context.settings.ToDictionary(Key=>Key.Key, val=>val.Value);
            return View(await Task.FromResult(settings));
        }


    }
}
