using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RimMenswearShop.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet]
        [Route("Error/{StatusCode}")]
        public IActionResult PageNotFound(int StatusCode)
        {
            ViewBag.ErrorMessage = "Lỗi 404: không tìm thấy trang!";
            return View();
        }
    }
}
