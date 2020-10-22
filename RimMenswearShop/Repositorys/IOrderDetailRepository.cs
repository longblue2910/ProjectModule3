using RimMenswearShop.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Repositorys
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetails> Get();

        IEnumerable<OrderDetails> Get(string orderId);

        OrderDetails Create(OrderDetails orderDetail);

        OrderDetails Edit(OrderDetails orderDetail);

        bool Remove(string orderId);
    }
}
