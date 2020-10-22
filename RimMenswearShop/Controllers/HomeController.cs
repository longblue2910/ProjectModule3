using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RimMenswearShop.Models;
using RimMenswearShop.Models.Cart;
using RimMenswearShop.Models.UserModel;
using RimMenswearShop.Repositorys;
using RimMenswearShop.Services;
using RimMenswearShop.ViewModels;

namespace RimMenswearShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository productRepository;
        private readonly IBrandRepository brandRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly AppDbContext context;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOrderRepository orderRepository;
        private readonly IContactRepository contactRepository;
        private const string CartSession = "CartSession";

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository,
                               IBrandRepository brandRepository, ICategoryRepository categoryRepository,
                               AppDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                               IOrderRepository orderRepository, IContactRepository contactRepository)
        {
            _logger = logger;
            this.productRepository = productRepository;
            this.brandRepository = brandRepository;
            this.categoryRepository = categoryRepository;
            this.context = context;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.orderRepository = orderRepository;
            this.contactRepository = contactRepository;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetObjectAsJson(CartSession,new List<Item>());

            ViewBag.Brands = brandRepository.Get().ToList();
            ViewBag.Categories = categoryRepository.Get().ToList();
            ViewBag.Products = productRepository.Get().ToList();
            return View();
        }
        public IActionResult Brand(int id, int page = 1)
        {
            var products = (from p in context.Products where p.BrandId == id && p.Remain > 0 select p).ToList();
            ViewBag.Categories = (from c in context.Categories select c).ToList();
            ViewBag.Brands = (from b in context.Brands select b).ToList();
            var brands = (from b in context.Brands where b.BrandId == id select b).ToList();
            if (brands.Count != 0)
                ViewBag.title = brands.FirstOrDefault().Name;
            else
                ViewBag.title = "Không có thương hiệu!";
            ViewBag.BrandId = id;
            ViewBag.Count = products.Count;
            ViewBag.Page = page;
            return View(products.Skip(page * 12 - 12).Take(12).ToList());
        }
        public IActionResult Category(int id, int page = 1)
        {
            var products = (from p in context.Products where p.CategoryId == id && p.Remain > 0 select p).ToList();
            ViewBag.Categories = (from c in context.Categories select c).ToList();
            ViewBag.Brands = (from b in context.Brands select b).ToList();
            var categories = (from c in context.Categories where c.CategoryId == id select c).ToList();
            if (categories.Count != 0)
                ViewBag.title = categories.FirstOrDefault().Name;
            else
                ViewBag.title = "Không có danh mục!";

            ViewBag.CategoryId = id;
            ViewBag.Count = products.Count;
            ViewBag.Page = page;
            return View(products.Skip(page * 12 - 12).Take(12).ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Details(string id)
        {
            var product = productRepository.Get(id);
            if (product == null)
            {
                ViewBag.ErrorMessage = "Không tìm thấy sản phẩm";
                return View("~/Views/Error/PageNotFound.cshtml");
            }
            ViewBag.Images = (from e in context.Images
                              where e.ProductId == product.ProductId
                              select e).ToList();
            var relatedProducts = (from p in context.Products
                                   where p.CategoryId == product.CategoryId ||
                                   p.BrandId == product.BrandId
                                   select p).ToList();
            relatedProducts.Remove(context.Products.Find(id));
            ViewBag.Brands = brandRepository.Get().ToList();
            ViewBag.Categories = categoryRepository.Get().ToList();
            ViewBag.RelatedProducts = relatedProducts;
            return View(product);
        }
        public IActionResult About()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var contact = new Contact()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Subject = model.Subject,
                    Message = model.Message
                };
                var newContact = contactRepository.Create(contact);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
