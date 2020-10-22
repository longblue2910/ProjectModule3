using RimMenswearShop.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Services
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> Get();

        Brand Get(int id);

        Brand Create(Brand brand);

        Brand Edit(Brand brand);

        bool Remove(int id);
    }
}
