using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.ViewModels.User
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Hãy nhập email !")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng !")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hãy nhập số điện thoại !")]
        [RegularExpression(@"^\(?(0|[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Hãy nhập mật khẩu !")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Hãy nhập xác nhận mật khẩu !")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Hãy chọn role!")]
        [Display(Name = "Roles")]
        public string RolesID { get; set; }
    }
}
