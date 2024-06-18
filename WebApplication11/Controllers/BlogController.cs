using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            
            return View(fiorelloDbContext.blogs.AsNoTracking().OrderByDescending(b => b.Id).ToList());
        }
        public IActionResult Detail(int? id)
        {
            if(id is null)
            {
                BadRequest();
            }
            var existedBlog = fiorelloDbContext.blogs.AsNoTracking().FirstOrDefault(b => b.Id == id);
            if(existedBlog == null)
            {
                return NotFound();
            }
            return View(existedBlog);
        }
    }
}
