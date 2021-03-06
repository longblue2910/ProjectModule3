﻿using RimMenswearShop.Models.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.ViewModels.Order
{
    public class OrderDetailViewModel
    {
        public string OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime CompleteTime { get; set; }

        [Required(ErrorMessage = "Chưa chọn trạng thái đơn hàng!")]
        public OrderStatus OrderStatus { get; set; }

        public string ProductId { get; set; }
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Chưa nhập giá sản phẩm!")]
        [Range(1, int.MaxValue, ErrorMessage = "Nhập sai giá!")]
        public int ProductPrice { get; set; }
        [Required(ErrorMessage = "Chưa nhập vào tên người nhận hàng!")]
        public string CustomerName { get; set; }
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int WardId { get; set; }
        public string WardName { get; set; }
        [Required(ErrorMessage = "Chưa nhập vào địa chỉ giao hàng!")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Chưa nhập vào số điện thoại người nhận hàng!")]
        [RegularExpression(@"^\(?(0|[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string CustomerPhoneNumber { get; set; }
            
        public string Note { get; set; }
        public int Quantity { get; set; }

    }
}
