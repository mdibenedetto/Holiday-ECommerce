using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace dream_holiday.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index(int id)
        {
            List<OrdersModel> banana = GetData(id);            

            return View(banana);
        }

        private List<OrdersModel> GetData(int id)
        {
            var list = new List<OrdersModel>();

            for (var i = 0; i < 10; i++)
            {
                list.Add(new OrdersModel
                {
                    nr = i + 1,
                    Id = 12547 + i,
                    Date = DateTime.Now.AddDays(i),
                   // .Now.AddDays(i),
                    Price = (decimal)1299.99 + (i * 2),
                    Qty = i + 2,
                    Status = i % 2 == 0
                });
 
            }

            // FILTER DATA - if/else
            return list; 

        }
    }
}
