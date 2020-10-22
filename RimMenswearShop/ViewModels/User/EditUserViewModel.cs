using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.ViewModels.User
{
    public class EditUserViewModel : CreateUserViewModel
    {
        public string UserId { get; set; }
    }
}
