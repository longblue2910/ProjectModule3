using Microsoft.EntityFrameworkCore;
using RimMenswearShop.Models;
using RimMenswearShop.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Repositorys
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly AppDbContext context;

        public OrderDetailRepository(AppDbContext context)
        {
            this.context = context;
        }
        public OrderDetails Create(OrderDetails orderDetail)
        {
            orderDetail.OrderDetailId = Guid.NewGuid().ToString();
            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();
            return orderDetail;
        }

        public OrderDetails Edit(OrderDetails orderDetail)
        {
            var editOrderDetail = context.OrderDetails.Attach(orderDetail);
            editOrderDetail.State = EntityState.Modified;
            context.SaveChanges();
            return orderDetail;
        }

        public IEnumerable<OrderDetails> Get()
        {
            return context.OrderDetails;
        }

        public IEnumerable<OrderDetails> Get(string orderId)
        {
            var orderDetails = (from o in context.OrderDetails where o.OrderId == orderId select o).ToList();
            return orderDetails;
        }

        public bool Remove(string orderId)
        {
            var orderDetailsToRemove = (from o in context.OrderDetails where o.OrderId == orderId select o).ToList();
            if (orderDetailsToRemove.Count > 0)
            {
                foreach (var o in orderDetailsToRemove)
                {
                    context.OrderDetails.Remove(o);
                    context.Products.Find(o.ProductId).Remain += o.Quantity;
                }

                context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
