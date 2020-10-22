using Microsoft.EntityFrameworkCore;
using RimMenswearShop.Models;
using RimMenswearShop.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Repositorys
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Order Create(Order order)
        {
            order.OrderId = Guid.NewGuid().ToString();
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }

        public Order Edit(Order order)
        {
            var editOrder = context.Orders.Attach(order);
            editOrder.State = EntityState.Modified;
            context.SaveChanges();
            return order;
        }

        public IEnumerable<Order> Get()
        {
            return context.Orders;
        }

        public Order Get(string id)
        {
            var order = (from e in context.Orders
                         where e.OrderId == id
                         select e).FirstOrDefault();
            return order;
        }

        public IEnumerable<District> GetDistricts(int provinceId)
        {
            return context.district.Where(e => e._province_id == provinceId);
        }

        public IEnumerable<Province> GetProvinces()
        {
            return context.province;
        }

        public IEnumerable<Ward> GetWards(int districtId = 0, int provinceId = 0)
        {
            if (provinceId != 0 && districtId != 0)
            {
                return context.ward.Where(e => e._province_id == provinceId && e._district_id == districtId);
            }
            else if (districtId != 0)
            {
                return context.ward.Where(e => e._district_id == districtId);
            }
            else if (provinceId != 0)
            {
                return context.ward.Where(e => e._province_id == provinceId);
            }
            return context.ward;
        }

        public bool Remove(string id)
        {
            var orderToRemove = context.Orders.Find(id);
            if (orderToRemove != null)
            {
                context.Orders.Remove(orderToRemove);
                return context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
