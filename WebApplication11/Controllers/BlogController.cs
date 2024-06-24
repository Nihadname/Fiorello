using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            ViewBag.BlogCount = fiorelloDbContext.blogs.Count();
            return View(fiorelloDbContext.blogs.AsNoTracking().OrderByDescending(b => b.Id).Take(3).ToList());
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
        public IActionResult LoadMore(int skip=3){
            var datas=fiorelloDbContext.blogs.AsNoTracking().Skip(skip).Take(3).ToList();
            ViewBag.BlogCount = fiorelloDbContext.blogs.Count();
            return   PartialView("_BlogPartialView", datas);
        }
    }
}
