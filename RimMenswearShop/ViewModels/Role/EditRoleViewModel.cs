using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.ViewModels.Role
{
    public class EditRoleViewModel 
    {
        public string RoleId { get; set; }
        [Required]
        public string NameRole { get; set; }
    }
}
