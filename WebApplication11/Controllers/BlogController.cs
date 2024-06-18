using Microsoft.AspNetCore.Mvc;
using WebApplication11.Data;

namespace WebApplication11.Controllers
{
    public class BlogController : Controller
    {
        private readonly FiorelloDbContext fiorelloDbContext;
        public BlogController(FiorelloDbContext fiorelloDb)
        {
            fiorelloDbContext = fiorelloDb;
        }
        public IActionResult Index()
        {
            
            return View(fiorelloDbContext.blogs.ToList());
        }
    }
}
