using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Models;
using Microsoft.AspNetCore.Mvc;

namespace dream_holiday.Controllers
{
    public class OrderDetailsController : Controller
    {    

        public IActionResult Index(int orderId)
        {
            var banana = GetData(orderId);
            return View(banana);
        }

        private OrderDetail GetData(int orderId)
        {
            var orderItem = new OrderDetail(); 

            orderItem.OrderId = orderId;
            orderItem.OrderDate = DateTime.Now.AddMonths(-1);
            //    DateTime.Now.ToString("dddd, dd MMMM yyyy");

            var list = new List<OrderDetailItem>();

            for (var i = 0; i < 6; i++)
            {
                list.Add(new OrderDetailItem
                {
                    nr = i + 1,
                    title = orderId +  " - Croatia Blue Lagoon",
                    description = "(Our half day tour from Split we take you to the most famous and the most popular Blue Lagoon in Croatia.)", 
                    Qty = 1,
                    Price = (decimal)298 + (i * 2),
                }); 

            }

            orderItem.Items = list;

            return orderItem;
             
            /// end deta data        
        }

        ////---
    }
}
