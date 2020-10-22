using RimMenswearShop.Models;
using RimMenswearShop.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Repositorys
{
    public interface IOrderRepository
    {
        IEnumerable<Province> GetProvinces();
        IEnumerable<District> GetDistricts(int provinceId);
        IEnumerable<Ward> GetWards(int districtId=0, int provinceId=0);
        IEnumerable<Order> Get();

        Order Get(string id);

        Order Create(Order order);

        Order Edit(Order order);

        bool Remove(string id);
    }
}
