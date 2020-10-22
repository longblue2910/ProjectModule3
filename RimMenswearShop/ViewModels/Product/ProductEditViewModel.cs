using RimMenswearShop.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.ViewModels.Product
{
    public class ProductEditViewModel : ProductCreateViewModel
    {
        public string ProductId { get; set; }
        public IEnumerable<Image> ImagesFileName { get; set; }
    }
}
