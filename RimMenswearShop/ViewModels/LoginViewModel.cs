using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Không được để trống")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hãy nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}
