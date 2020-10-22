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
    public interface IProductRepository
    {
        IEnumerable<Product> Get();

        ProductDetailViewModel Get(string id);

        Product Create(ProductCreateViewModel product);

        Product Edit(ProductEditViewModel product);

        bool Remove(string id);
    }
}
