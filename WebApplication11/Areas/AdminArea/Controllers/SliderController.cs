using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Areas.AdminArea.ViewModels.Slider;
using WebApplication11.Data;
using WebApplication11.Extensions;
using WebApplication11.Models;

namespace WebApplication11.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly FiorelloDbContext _fiorelloDbContext;

        public SliderController(FiorelloDbContext fiorelloDbContext)
        {
            _fiorelloDbContext = fiorelloDbContext;
        }

        public async  Task<IActionResult> Index()
        {
            var sliders= await _fiorelloDbContext.sliders.AsNoTracking().ToListAsync();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM sliderCreate)
        {
            var file = sliderCreate.sliderImage;
            if (file == null)
            {
                ModelState.AddModelError("ProfileImage", "Image cannot be null.");
                return View(sliderCreate);
            }
            if (!file.CheckContentType())
            {
                ModelState.AddModelError("ProfileImage", "Only image files are allowed.");
                return View(sliderCreate);
            }
            if (!file.CheckSize(500))
            {
                ModelState.AddModelError("ProfileImage", "The image size is too large. Maximum allowed size is 500KB.");
                return View(sliderCreate);
            }
            Slider slider = new Slider();
            slider.ImageUrl = await file.SaveFile();
      await   _fiorelloDbContext.sliders.AddAsync(slider);
            await _fiorelloDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return BadRequest();
            var existedSlider= await _fiorelloDbContext.sliders.AsNoTracking().FirstOrDefaultAsync(s=>s.Id==id);
            if(existedSlider is null) return NotFound();
            existedSlider?.ImageUrl.DeleteFile();
            _fiorelloDbContext.sliders.Remove(existedSlider);
            _fiorelloDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var existedSlider= await _fiorelloDbContext.sliders.AsNoTracking().FirstOrDefaultAsync(s=>s.Id==id);
            if(existedSlider is null) return NotFound();
            return View(new SliderUpdateVM { ImageUrl=existedSlider.ImageUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public  async Task<IActionResult> Update(int? id, SliderUpdateVM sliderUpdateVm)
        {
            if (id == null) return BadRequest();
            var slider= await _fiorelloDbContext.sliders.AsNoTracking().FirstOrDefaultAsync(s=>s.Id==id);
            if (slider is null) return NotFound();
            var file=sliderUpdateVm.photo;
            if (file == null)
            {
                ModelState.AddModelError("ProfileImage", "Image cannot be null.");
                sliderUpdateVm.ImageUrl = slider.ImageUrl;
                return View(sliderUpdateVm);
            }
            if (!file.CheckContentType())
            {
                ModelState.AddModelError("ProfileImage", "Only image files are allowed.");
                return View(sliderUpdateVm);
            }
            if (!file.CheckSize(500))
            {
                ModelState.AddModelError("ProfileImage", "The image size is too large. Maximum allowed size is 500KB.");
                return View(sliderUpdateVm);
            }
            var fileName=await file.SaveFile();
            slider.ImageUrl?.DeleteFile();
            slider.ImageUrl = fileName;
            _fiorelloDbContext.sliders.Update(slider);
            _fiorelloDbContext.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
