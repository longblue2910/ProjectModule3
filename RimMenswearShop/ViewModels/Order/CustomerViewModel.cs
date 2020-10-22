using RimMenswearShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.ViewModels.Order
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            Provinces = new List<Province>();
            Districts = new List<District>();
            Wards = new List<Ward>();
        }

        public IEnumerable<Province> Provinces { get; set; }
        public IEnumerable<District> Districts { get; set; }
        public IEnumerable<Ward> Wards { get; set; }
        [Required(ErrorMessage = "Nhập vào tên người nhận hàng!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nhập vào số điện thoại người nhận hàng!")]
        [RegularExpression(@"^\(?(0|[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Tỉnh/Thành phố")]
        public int ProvinceId { get; set; }
        [Display(Name = "Quận/Huyện")]
        public int DistrictId { get; set; }
        [Display(Name = "Phường/Xã")]
        public int WardId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
    }
}
