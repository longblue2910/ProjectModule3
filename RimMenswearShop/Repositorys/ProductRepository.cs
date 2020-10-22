using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RimMenswearShop.Models;
using RimMenswearShop.Models.Products;
using RimMenswearShop.ViewModels;
using RimMenswearShop.ViewModels.Product;
using RimMenswearShop.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IImageRepository imageRepository;

        public ProductRepository(AppDbContext context, IWebHostEnvironment webHostEnvironment,
                                  IImageRepository imageRepository)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.imageRepository = imageRepository;
        }
        public Product Create(ProductCreateViewModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Price = model.Price,
                Remain = model.Remain,
            };
            product.IsDeleted = false;
            product.CreatedTime = DateTime.Now;
            product.ProductId = Guid.NewGuid().ToString();
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product Edit(ProductEditViewModel model)
        {

            var product = new Product
            {
                ProductId = model.ProductId,
                Name = model.Name,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
                Description = model.Description,
                Price = model.Price,
                Remain = model.Remain,
            };
            var editProduct = context.Products.Attach(product);
            editProduct.State = EntityState.Modified;
            context.SaveChanges();
            return product;
        }

        public IEnumerable<Product> Get()
        {
            return (from p in context.Products where p.IsDeleted == false select p).OrderByDescending(
                p => p.Name);
        }

        public ProductDetailViewModel Get(string id)
        {
            var data = (from e in context.Products
                        where e.IsDeleted == false
                        join d in context.Categories on e.CategoryId equals d.CategoryId
                        join f in context.Brands on e.BrandId equals f.BrandId
                        where e.ProductId == id
                        select new ProductDetailViewModel
                        {
                            ProductId = e.ProductId,
                            BrandId = e.BrandId,
                            BrandName = f.Name,
                            CategoryId = e.CategoryId,
                            CategoryName = d.Name,
                            Description = e.Description,
                            Name = e.Name,
                            Price = e.Price,
                            Remain = e.Remain
                        }).FirstOrDefault();
            return data;
        }

        public bool Remove(string id)
        {
            var productToRemove = context.Products.Find(id);
            if (productToRemove != null)
            {
                productToRemove.IsDeleted = true;
                return context.SaveChanges() > 0;
            }

            return false;
        }
    }
}
