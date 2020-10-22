using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Không được để trống")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="Hãy nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name ="Mật khẩu")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
