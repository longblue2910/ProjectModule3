using RimMenswearShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Repositorys
{
    public interface IContactRepository
    {
        Contact Create(Contact contact);
    }
}
