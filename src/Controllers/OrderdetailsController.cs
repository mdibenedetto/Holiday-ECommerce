using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dream_holiday.Controllers
{
    [Authorize]
    public class OrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index(int orderId)
        {     
            var order = (from od in _context.Order
                         where od.Id == orderId
                         select od)
                         .FirstOrDefault();

            ViewBag.orderId = orderId;
            ViewBag.orderDate = order?.Date;

            var orderDetails =
                (from od in _context.OrderDetail
                    join tp in _context.TravelPackage
                    on od.TravelPackage.Id equals tp.Id
                 where od.Order.Id == orderId
                 select new OrderDetailModel
                {
                    OrderDetail = od,
                    TravelPackage = tp
                }).ToList(); 

            return View(orderDetails);
        }

    }
}
