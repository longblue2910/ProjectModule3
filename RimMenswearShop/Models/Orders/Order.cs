using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Models.Orders
{
    public class Order
    {
        [Required] public string OrderId { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public OrderStatus Status { get; set; }

        [Required] public DateTime OrderTime { get; set; }

        public DateTime CompleteTime { get; set; }
        public string Note { get; set; }
        public int Quantity { get; set; }

    }
}
