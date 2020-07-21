using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using Microsoft.AspNetCore.Mvc;

namespace dream_holiday.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index(int orderId)
        {
            // todo: remove it when cart is ready
            this.MockData(orderId);

     
            var order = (from od in _context.Order
                         where od.Id == orderId
                         select od)
                         .FirstOrDefault();

            ViewBag.orderId = orderId;
            ViewBag.orderDate = order.Date;

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



        private void MockData(int orderId)
        {
            //_context.OrderDetail.Clear();
            //_context.SaveChanges();

            if (_context.OrderDetail.Any(order => order.Order.Id == orderId) )
            {
                return;
            }

            //var order = _context.Order.Find(orderId);
            var list = new List<OrderDetail>();

            for (var i = 0; i < 6; i++)
            {
                var orderItem = new OrderDetail();

                orderItem.OrderId = orderId;
                orderItem.Id = i + 1;
                orderItem.TravelPackage = _context
                    .TravelPackage
                    .ToList()[i];
            
                list.Add(orderItem);
            }

            _context.OrderDetail.AddRange(list);
            _context.SaveChanges();

        } 
       
    }
}
