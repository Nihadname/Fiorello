using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Data;
using WebApplication11.Data.Migrations;
using WebApplication11.Models;

namespace WebApplication11.ViewCompenents
{
    public class ProductViewComponent :ViewComponent
    {
        private readonly FiorelloDbContext fiorelloDbContext;
        public ProductViewComponent(FiorelloDbContext context)
        {
            fiorelloDbContext = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int take=4)
        {
            var products = fiorelloDbContext.products
                                                     .Include(p => p.Category)
                                                     .Include(p => p.Images)
                                                     .Take(take).ToList();
            return View(await Task.FromResult(products));
        }

    }
}
