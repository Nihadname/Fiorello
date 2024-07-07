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
            var  query=fiorelloDbContext.blogs;
            ViewBag.BlogCount = query.Count();
            return View();
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
            return   PartialView("_BlogPartialView", datas);
        }
        public async Task<IActionResult> Search(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Search text cannot be null or empty.");
            }
            var datas = await fiorelloDbContext.blogs.Where(s=>s.Title.ToLower().Contains(text.ToLower())).Take(4).ToListAsync();

            return PartialView("_SearchPartialView", datas);

        }
    }
}
