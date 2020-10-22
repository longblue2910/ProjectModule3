using Microsoft.AspNetCore.Http;
using RimMenswearShop.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Chưa nhập tên sản phẩm!")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Chưa nhập giá!")]
        [Range(1, int.MaxValue, ErrorMessage = "Nhập sai giá!")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Chưa chọn thương hiệu!")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Chưa nhập vào số lượng trong kho!")]
        [Range(1, int.MaxValue, ErrorMessage = "Nhập sai số lượng!")]
        public int? Remain { get; set; }

        [Required(ErrorMessage = "Chưa chọn loại sản phẩm!")]
        public int CategoryId { get; set; }

        public IEnumerable<IFormFile> ImageFiles { get; set; }
        public string Description { get; set; }
    }
}
