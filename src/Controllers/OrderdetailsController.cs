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

            var orderDetails = _context
                .OrderDetail
                .ToList();

            //var list = GetData(orderId);


            return View(orderDetails);
        }



        private void MockData(int orderId)
        {
            if (_context.OrderDetail.Any(order => order.Order.Id == orderId) )
            {
                return;
            }

     

               // new Order { Id = orderId };
          
            //orderItem.OrderDate = DateTime.Now.AddMonths(-1);
            //    DateTime.Now.ToString("dddd, dd MMMM yyyy");
            var order = _context.Order.Find(orderId);
            var list = new List<OrderDetail>();

            for (var i = 0; i < 6; i++)
            {
                var orderItem = new OrderDetail();

                orderItem.Order = order;
                orderItem.Id = orderId + i;

                orderItem.Package = new TravelPackage
                {                   
                    Name = orderId + " - Croatia Blue Lagoon",
                    Description = "(Our half day tour from Split we take you to the most famous and the most popular Blue Lagoon in Croatia.)",
                    Qty = 1,
                    Price = (decimal)298 + (i * 2),
                };

                list.Add(orderItem);
            }

            _context.OrderDetail.AddRange(list);
            _context.SaveChanges();

        }




        private OrderDetail GetData(int orderId)
        {
           
            var orderItem = new OrderDetail(); 

            orderItem.Id = orderId;
            orderItem.OrderDate = DateTime.Now.AddMonths(-1);
            //    DateTime.Now.ToString("dddd, dd MMMM yyyy");

            var list = new List<TravelPackage>();

            for (var i = 0; i < 6; i++)
            {
                list.Add(new TravelPackage
                {
                    Id = i + 1,
                    Name = orderId +  " - Croatia Blue Lagoon",
                    Description = "(Our half day tour from Split we take you to the most famous and the most popular Blue Lagoon in Croatia.)", 
                    Qty = 1,
                    Price = (decimal)298 + (i * 2),
                }); 

            }

           
            return orderItem;   
        }
 
       
    }
}
