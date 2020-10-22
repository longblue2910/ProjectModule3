using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Không được để trống!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Không được để trống!")]
        public string Message { get; set; }
    }
}
