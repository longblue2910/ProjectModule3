using RimMenswearShop.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Repositorys
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();

        Customer Get(string id);

        Customer Create(Customer customer);

        Customer Edit(Customer customer);

        bool Remove(string id);
    }
}
