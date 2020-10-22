using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RimMenswearShop.Models;
using RimMenswearShop.Models.Products;
using RimMenswearShop.Services;
using RimMenswearShop.ViewModels;
using RimMenswearShop.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace RimMenswearShop.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IBrandRepository brandRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IImageRepository imageRepository;
        private readonly AppDbContext context;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository,
                                 IBrandRepository brandRepository, IWebHostEnvironment webHostEnvironment, IImageRepository imageRepository,
                                 AppDbContext context)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.imageRepository = imageRepository;
            this.context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Brands = GetBrands();
            ViewBag.Products = productRepository.Get().ToList();
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            ViewBag.Brands = GetBrands();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {              
                var createProduct = productRepository.Create(model);

                if (model.ImageFiles != null)
                {
                    var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\products");
                    foreach (var imageFile in model.ImageFiles)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                        var filePath = Path.Combine(uploadFolder, fileName);
                        using var fs = new FileStream(filePath, FileMode.Create);
                        imageFile.CopyTo(fs);

                        imageRepository.Create(new Image
                        {
                            ImageId = Guid.NewGuid().ToString(),
                            ProductId = createProduct.ProductId,
                            ImageName = fileName
                        });
                    }
                }

                return RedirectToAction("Details", "Home", new { id = createProduct.ProductId });
            }
            ViewBag.Categories = categoryRepository.Get();
            ViewBag.Brands = brandRepository.Get();
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            ViewBag.Categories = categoryRepository.Get();
            ViewBag.Brands = brandRepository.Get();
            var product = productRepository.Get(id);
            ViewBag.Images = (from e in context.Images
                              where e.ProductId == product.ProductId
                              select e).ToList();
            if (product == null) return View("~/Views/Error/PageNotFound.cshtml");
            var editProduct = new ProductEditViewModel
            {
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Description = product.Description,
                //ImageFileName = product.ImageFileName,
                Name = product.Name,
                Price = product.Price,
                ProductId = product.ProductId,
                Remain = product.Remain
            };
            return View(editProduct);
        }
        [HttpPost]
        public IActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var editProduct = productRepository.Edit(model);
                if (model.ImageFiles != null)
                {
                    var uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images\\products");
                    foreach (var imageFile in model.ImageFiles)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                        var filePath = Path.Combine(uploadFolder, fileName);
                        using var fs = new FileStream(filePath, FileMode.Create);
                        imageFile.CopyTo(fs);

                        imageRepository.Create(new Image
                        {
                            ImageId = Guid.NewGuid().ToString(),
                            ProductId = editProduct.ProductId,
                            ImageName = fileName
                        });
                    }
                }
                if (editProduct != null)
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            ViewBag.Categories = categoryRepository.Get();
            ViewBag.Brands = brandRepository.Get();
            return View();
        }
        private List<Brand> GetBrands()
        {
            return brandRepository.Get().ToList();
        }
        private List<Category> GetCategories()
        {
            return categoryRepository.Get().ToList();
        }
        public IActionResult Remove(string id)
        {
            if (productRepository.Remove(id))
            {
                return RedirectToAction("Index", "Product");
            }
            return View();
        }
    }
}
