using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Models
{
    public class Province
    {
        public int id { get; set; }
        public string _name { get; set; }
        public string _code { get; set; }
        public ICollection<District> Districts { get; set; }
    }
}
