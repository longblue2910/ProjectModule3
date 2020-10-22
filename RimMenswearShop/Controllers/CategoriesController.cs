using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RimMenswearShop.Models;
using RimMenswearShop.Models.Products;
using RimMenswearShop.Services;

namespace RimMenswearShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly AppDbContext context;

        public CategoriesController(ICategoryRepository categoryRepository, AppDbContext context)
        {
            this.categoryRepository = categoryRepository;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(categoryRepository.Get());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Create(category);
                return RedirectToAction("Index", "Categories");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = categoryRepository.Get(id);
            if (category == null)
            {
                return View("~/View/Error/PageNotFound.cshtml");
            }
            ViewBag.NumberOfProducts = (from p in context.Products where p.CategoryId == id select p).ToList().Count;
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (categoryRepository.Edit(category) != null)
            {
                return RedirectToAction("Index", "Categories");
            }
            return View();
        }
        public IActionResult Remove(int id)
        {
            var category = categoryRepository.Get(id);
            if (category == null)
            {
                return View("~/Views/Error/PageNotFound.cshtml");
            }
            if (categoryRepository.Remove(id))
            {
                return RedirectToAction("Index", "Categories");
            }      
            return RedirectToAction("Index", "Categories");
        }
    }
}
