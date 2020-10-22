﻿using RimMenswearShop.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Services
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Get();

        Category Get(int id);

        Category Create(Category category);

        Category Edit(Category category);

        bool Remove(int id);
    }
}
