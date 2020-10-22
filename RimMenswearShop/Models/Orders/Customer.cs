using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Models.Orders
{
    public class Customer
    {
        [Required] public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        [Required] public string PhoneNumber { get; set; }

        public string Password { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
    }

}
