using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RimMenswearShop.Models;
using RimMenswearShop.Models.Cart;
using RimMenswearShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Controllers
{
    [AllowAnonymous]
    public class CartController : Controller
    {        
        private const string CartSession = "CartSession";
        private readonly AppDbContext context;
        private readonly IProductRepository productRepository;

        public CartController(AppDbContext context, IProductRepository productRepository)
        {
            this.context = context;
            this.productRepository = productRepository;
        }
        public IActionResult Index(string id)
        {
            ViewBag.Products = productRepository.Get().ToList();
            ViewBag.ProductId = id;
            ViewBag.Product = (from p in context.Products where p.IsDeleted == false && p.ProductId == id select p)
                .ToList().FirstOrDefault();
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>(CartSession);
            HttpContext.Session.SetObjectAsJson(CartSession, cart);

            return View(cart);
        }

        [Route("/Cart/AddItem/{id}")]
        public IActionResult AddItem(string id)
        {
            Item item = new Item() { ProductId = id, Amount = 1 };
            var cart = HttpContext.Session.GetObjectFromJson<List<Item>>(CartSession);

            if (cart.Count != 0)
            {
                if (cart.Exists(el => el.ProductId == item.ProductId))
                {
                    cart.FirstOrDefault(el => el.ProductId == item.ProductId).Amount += 1;
                    HttpContext.Session.SetObjectAsJson(CartSession, cart);
                    return Json(-1);
                }
                else
                {
                    cart.Add(item);
                    HttpContext.Session.SetObjectAsJson(CartSession, cart);
                    return Json(cart.Count);
                }
            }
            cart.Add(item);
            HttpContext.Session.SetObjectAsJson(CartSession, cart);

            return Json(cart.Count);
        }
    }
}
