using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace RimMenswearShop.Models.Products
{
    public class Product
    {
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Chưa nhập tên sản phẩm!")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Chưa nhập giá!")]
        [Range(1, int.MaxValue, ErrorMessage = "Nhập sai giá!")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Chưa chọn thương hiệu!")]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        [Required(ErrorMessage = "Chưa nhập vào số lượng trong kho!")]
        [Range(1, int.MaxValue, ErrorMessage = "Nhập sai số lượng!")]
        public int? Remain { get; set; }

        [Required(ErrorMessage = "Chưa chọn loại sản phẩm!")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required(ErrorMessage = "Chưa upload ảnh sản phẩm!")]
        public IEnumerable<Image> Images { get; set; }

        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
