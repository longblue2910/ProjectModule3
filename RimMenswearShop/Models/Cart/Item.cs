using RimMenswearShop.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Models.Cart
{
    public class Item
    {
        public string ProductId { get; set; }
        public int Amount { get; set; }
    }
}
