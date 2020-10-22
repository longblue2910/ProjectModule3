using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RimMenswearShop.Models;
using RimMenswearShop.Models.Orders;
using RimMenswearShop.Repositorys;
using RimMenswearShop.Services;
using RimMenswearShop.ViewModels.Order;

namespace RimMenswearShop.Controllers
{
    
    public class OrdersController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IBrandRepository brandRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly AppDbContext context;
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderDetailRepository orderDetailRepository;

        public OrdersController(IProductRepository productRepository, IBrandRepository brandRepository,
                                ICategoryRepository categoryRepository, AppDbContext context, IOrderRepository orderRepository,
                                ICustomerRepository customerRepository, IOrderDetailRepository orderDetailRepository)
        {
            this.productRepository = productRepository;
            this.brandRepository = brandRepository;
            this.categoryRepository = categoryRepository;
            this.context = context;
            this.orderRepository = orderRepository;
            this.customerRepository = customerRepository;
            this.orderDetailRepository = orderDetailRepository;
        }
        [HttpGet]
        public IActionResult Order(string id)
        {
            var product = (from p in context.Products where p.IsDeleted == false && p.ProductId == id select p)
                .ToList().FirstOrDefault();
            if (product.Remain == 0)
                return RedirectToAction("Index", "Home");
            var customer = new CustomerViewModel();
            customer = new CustomerViewModel
            {
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                ProductId = id,
            };
            ViewBag.Product = product;
            customer.Provinces = orderRepository.GetProvinces();
            //customer.ProvinceId = defaultProvinceId;
            customer.Districts = orderRepository.GetDistricts(customer.ProvinceId);
            //customer.DistrictId = defaultDistrictId;
            customer.Wards = orderRepository.GetWards(customer.DistrictId, customer.ProvinceId);
            return View(customer);
        }
        [HttpPost]
        public IActionResult Order(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product =
                    (from p in context.Products where p.IsDeleted == false && p.ProductId == model.ProductId select p)
                    .ToList().FirstOrDefault();
                var customer = new Customer
                {
                    CustomerName = model.Name,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    ProvinceId = model.ProvinceId,
                    WardId = model.WardId,
                    DistrictId = model.DistrictId,
                };
                var createCustomer = customerRepository.Create(customer);
                var order = new Order
                {
                    CustomerId = createCustomer.CustomerId,
                    OrderTime = DateTime.Now,
                    Status = OrderStatus.Pending,
                    Quantity = model.Quantity
                };
                var createOrder = orderRepository.Create(order);
                var orderDetail = new OrderDetails
                {
                    OrderId = createOrder.OrderId,
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = order.Quantity
                };
                var createOrderDetail = orderDetailRepository.Create(orderDetail);
                context.Products.Find(model.ProductId).Remain -= 1;
                context.SaveChanges();
                return RedirectToAction("OrderDetail", new { id = createOrder.OrderId });
            }

            return View();
        }
        public IActionResult OrderDetail(string id)
        {
            var orderDetail = (from o in context.Orders
                               where o.OrderId == id
                               join d in context.OrderDetails on o.OrderId equals d.OrderId
                               join c in context.Customers on o.CustomerId equals c.CustomerId
                               join p in context.Products on d.ProductId equals p.ProductId
                               join t in context.province on c.ProvinceId equals t.id
                               join q in context.district on c.DistrictId equals q.id
                               join w in context.ward on c.WardId equals w.id
                               select new OrderDetailViewModel
                               {
                                   OrderId = o.OrderId,
                                   ProductId = p.ProductId,
                                   CustomerAddress = c.Address,
                                   CustomerName = c.CustomerName,
                                   CustomerPhoneNumber = c.PhoneNumber,
                                   OrderTime = o.OrderTime,
                                   CompleteTime = o.CompleteTime,
                                   ProductName = p.Name,
                                   ProductPrice = p.Price,
                                   OrderStatus = o.Status,
                                   Note = o.Note,
                                   ProvinceName = t._name,
                                   DistrictName = q._name,
                                   WardName = w._name,
                                   Quantity = o.Quantity               
                               }).ToList().FirstOrDefault();
            ViewBag.OrderId = id;
           
            return View(orderDetail);
        }
       
        [Route("/Orders/Districts/{provinceId}")]
        public IActionResult GetDistricts(int provinceId)
        {
            var districts = orderRepository.GetDistricts(provinceId);
            return Json(new { districts });
        }

        [Route("/Orders/Wards/{districtId}")]
        public IActionResult GetWards(int districtId, int provinceId)
        {
            var wards = orderRepository.GetWards(districtId, provinceId);
            return Json(new { wards });
        }

        [Route("/Orders/BuyWithQuantity/{productId}/{quantity}")]
        public IActionResult BuyWithQuantity(string productId,int quantity)
        {
            return Json(0);
        }
        [HttpGet]
        public IActionResult OrderCart(string productId)
        {
            var product = (from p in context.Products where p.IsDeleted == false && p.ProductId == productId select p)
                .ToList().FirstOrDefault();
            if (product.Remain == 0)
                return RedirectToAction("Index", "Home");
            var customer = new CustomerViewModel();
            customer = new CustomerViewModel
            {
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                ProductId = productId,
            };
            ViewBag.Product = product;
            customer.Provinces = orderRepository.GetProvinces();
            //customer.ProvinceId = defaultProvinceId;
            customer.Districts = orderRepository.GetDistricts(customer.ProvinceId);
            //customer.DistrictId = defaultDistrictId;
            customer.Wards = orderRepository.GetWards(customer.DistrictId, customer.ProvinceId);
            return View(customer);
        }
        [HttpPost]
        public IActionResult OrderCart(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product =
                    (from p in context.Products where p.IsDeleted == false && p.ProductId == model.ProductId select p)
                    .ToList().FirstOrDefault();
                var customer = new Customer
                {
                    CustomerName = model.Name,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    ProvinceId = model.ProvinceId,
                    WardId = model.WardId,
                    DistrictId = model.DistrictId,
                };
                var createCustomer = customerRepository.Create(customer);
                var order = new Order
                {
                    CustomerId = createCustomer.CustomerId,
                    OrderTime = DateTime.Now,
                    Status = OrderStatus.Pending,
                    Quantity = model.Quantity
                };
                var createOrder = orderRepository.Create(order);
                var orderDetail = new OrderDetails
                {
                    OrderId = createOrder.OrderId,
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = order.Quantity
                };
                var createOrderDetail = orderDetailRepository.Create(orderDetail);
                context.Products.Find(model.ProductId).Remain -= 1;
                context.SaveChanges();
                return RedirectToAction("OrderDetail", new { id = createOrder.OrderId });
            }

            return View();
        }
    }
}
