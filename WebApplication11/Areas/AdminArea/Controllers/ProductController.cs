using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication11.Controllers;
using WebApplication11.Extensions;
using WebApplication11.Models;
using WebApplication11.Repositories.interfaces;
using WebApplication11.ViewModels;

namespace WebApplication11.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly IRepository<Product> _repository;
        private readonly IRepository<Category> _categoryRepository;

        public ProductController(IRepository<Product> repository, IRepository<Category> categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _repository.GetAllAsync(0,0,s=>s.Images,s=>s.Category);
            return View(products);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return BadRequest("Product ID cannot be null.");

            var existedProduct = await _repository.GetByIdAsync(id.Value, s => s.Images, s => s.Category);

            if (existedProduct == null)
                return NotFound();

            return View(existedProduct);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(0, 0), "Id", "Name");
            return View();
        }
        public async Task<IActionResult> Create(ProductCreateVM productCreateVM)
        {

            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(0, 0), "Id", "Name");
            if (!ModelState.IsValid) return View(productCreateVM);
            var files = productCreateVM.Photos;
            if (files.Length == 0)
            {
                ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(0, 0), "Id", "Name");

                ModelState.AddModelError("Photos", "Oimages can not bu null");
                return View(productCreateVM);
            }
            Product newProduct = new Product();

            List<ProductImage> images = new List<ProductImage>();
            foreach (var newProfileImage in files)
            {

                if (!newProfileImage.CheckContentType())
                {
                    ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(0, 0), "Id", "Name");

                    ModelState.AddModelError("Photos", "Only image files are allowed.");
                    return View(productCreateVM);
                }
                if (!newProfileImage.CheckSize(10000))
                {
                    ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(0, 0), "Id", "Name");

                    ModelState.AddModelError("Photos", "The image size is too large. Maximum allowed size is 500KB.");
                    return View(productCreateVM);
                }
                ProductImage newImage = new ProductImage();
                newImage.Name = await newProfileImage.SaveFile();
                newImage.ProductId = newProduct.Id;
                if (files[0] == newProfileImage)
                {
                    newImage.IsMain = true;
                }
                images.Add(newImage);


                // Save the new image file
                //existingUser.imageUrl = await newProfileImage.SaveFile();
                //var result = await _userManager.UpdateAsync(existingUser);
                //if (!result.Succeeded)
                //{
                //    ModelState.AddModelError("", "Failed to update user profile.");
                //    return View(blogCreateVM);
                //    ;
                //}

            }
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(0, 0), "Id", "Name");

            newProduct.Images = images;
            newProduct.CategoryId = productCreateVM.CategoryId;
            newProduct.Name = productCreateVM.Name;
            newProduct.Price = productCreateVM.Price;
            newProduct.Count = productCreateVM.Count;
          await  _repository.AddAsync(newProduct);  
            return RedirectToAction("Index");
        }
    }
}
