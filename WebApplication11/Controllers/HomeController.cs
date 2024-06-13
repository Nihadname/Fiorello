using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication11.Data;
using WebApplication11.Models;
using WebApplication11.ViewModels;

namespace WebApplication11.Controllers
{
    public class HomeController : Controller
    {
      private readonly FiorelloDbContext fiorelloDbContext;

        public HomeController(FiorelloDbContext fiorelloDb)
        {
                fiorelloDbContext = fiorelloDb;
        }
        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
           var sliders=fiorelloDbContext.sliders.AsNoTracking().ToList();
            var sliderContent=fiorelloDbContext.contents.AsNoTracking().FirstOrDefault();
            var categories=fiorelloDbContext.categories.AsNoTracking().ToList();
            var products=fiorelloDbContext.products.Include(s=>s.Images).Include(s=>s.Category).AsNoTracking().ToList();
            var abouts = fiorelloDbContext.abouts.AsNoTracking().FirstOrDefault();
            model.Sliders = sliders;
            model.Content = sliderContent;
            model.Categories = categories;
            model.products=products;
            model.About = abouts;
            return View(model);
        }

    }
}
