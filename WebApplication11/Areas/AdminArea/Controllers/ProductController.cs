using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication11.Controllers;
using WebApplication11.Data;
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
        private FiorelloDbContext appDbContext;

        public ProductController(IRepository<Product> repository, IRepository<Category> categoryRepository, FiorelloDbContext dbContext)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            appDbContext = dbContext;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public async Task<IActionResult> SetMainPhoto(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await appDbContext.productsImage.FirstOrDefaultAsync(x => x.Id == id);
            if (existedPhoto == null) return NotFound();

            var mainImage = await appDbContext.productsImage
                .FirstOrDefaultAsync(y => y.IsMain == true && y.ProductId == existedPhoto.ProductId);
            if (mainImage != null) mainImage.IsMain = false;

            existedPhoto.IsMain = true;
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Detail", new { id = existedPhoto.ProductId });
        }
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(0, 0), "Id", "Name");
            if (id == null) return BadRequest();
            var existedProduct = await _repository.GetByIdAsync(id, s => s.Images, s => s.Category);
            if (existedProduct == null) return NotFound();
            return View(new productUpdateVM
            {

                Name = existedProduct.Name,
                Price = existedProduct.Price,
                Count= existedProduct.Count,
                CategoryId = existedProduct.CategoryId,
                Images = existedProduct.Images,
            });
        }
        [HttpPost]
        public  async Task<IActionResult> Update(int? id , productUpdateVM model)
        {
            ViewBag.Categories = new SelectList(await _categoryRepository.GetAllAsync(0, 0), "Id", "Name");
            if (id == null) return BadRequest();
            var existedProduct = await _repository.GetByIdAsync(id, s => s.Images, s => s.Category);
            if (existedProduct == null) return NotFound();
            if (!ModelState.IsValid)
            {
                model.Images = existedProduct.Images;
                return View(model);
            }
            var files = model.Photos;
            model.Images = existedProduct.Images;
            if (files is not null)
            {
                if (files.Length > 4)
                {
                    model.Images = existedProduct.Images;
                    ModelState.AddModelError("Photos", "Maximum 4 Photos!");
                    return View(model);
                }

                List<ProductImage> list = new();
                foreach (var file in files)
                {
                    if (!file.CheckContentType())
                    {
                        ModelState.AddModelError("Photos", "Choose the right type!");
                        return View(model);
                    }

                    var blogImage = new ProductImage
                    {
                        Name = await file.SaveFile(),
                        ProductId = existedProduct.Id,
                     
                };
                    if (files[0] == file)
                    {
                        blogImage.IsMain = true;
                    }
                    list.Add(blogImage);
                }
                existedProduct.Images = list;
            }
            existedProduct.Name = model.Name;
            existedProduct.Count= model.Count;
            existedProduct.Price = model.Price;
            existedProduct.CategoryId = model.CategoryId;
            //appDbContext.Entry(existedProduct).State = EntityState.Modified;
            await _repository.UpdateAsync(existedProduct);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await appDbContext.productsImage.FirstOrDefaultAsync(x => x.Id == id);
            if (existedPhoto == null) return NotFound();
            existedPhoto.Name.DeleteFile();
            appDbContext.productsImage.Remove(existedPhoto);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Update", new { id = existedPhoto.ProductId });
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var existedPhoto = await appDbContext.products.Include(s=>s.Images).Include(s=>s.Category)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (existedPhoto == null) return NotFound();
            foreach (var image in existedPhoto.Images)
            {
                image.Name.DeleteFile();

            }
            appDbContext.products.Remove(existedPhoto);
            await appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
