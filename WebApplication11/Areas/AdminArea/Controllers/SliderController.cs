using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Areas.AdminArea.ViewModels.Slider;
using WebApplication11.Controllers;
using WebApplication11.Data;
using WebApplication11.Extensions;
using WebApplication11.Models;
using WebApplication11.Repositories.interfaces;

namespace WebApplication11.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : BaseController<Slider>
    {
       

        public SliderController(IRepository<Slider> repository) : base(repository)
        {
        }

        public async  Task<IActionResult> Index()
        {
            var sliders = await GetAllAsync();
            return View(sliders);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();
            var existedProduct = await GetByIdAsync(id);
            return View(existedProduct);
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
            await AddAsync(slider);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) return BadRequest();
            var existedSlider = await GetByIdAsync(id);
            if (existedSlider is null) return NotFound();
            existedSlider?.ImageUrl.DeleteFile();
            await DeleteAsync(existedSlider);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var existedSlider = await GetByIdAsync(id);
            if (existedSlider is null) return NotFound();
            return View(new SliderUpdateVM { ImageUrl=existedSlider.ImageUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public  async Task<IActionResult> Update(int? id, SliderUpdateVM sliderUpdateVm)
        {
            if (id == null) return BadRequest();
            var slider = await GetByIdAsync(id);
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
                sliderUpdateVm.ImageUrl = slider.ImageUrl;

                return View(sliderUpdateVm);
            }
          
            var fileName=await file.SaveFile();
            slider.ImageUrl?.DeleteFile();
            slider.ImageUrl = fileName;
            await UpdateAsync(slider);
            return RedirectToAction("Index");

        }
    }
}
