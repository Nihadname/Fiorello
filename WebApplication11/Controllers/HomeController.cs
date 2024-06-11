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
            model.Sliders = sliders;
            return View(model);
        }

    }
}
