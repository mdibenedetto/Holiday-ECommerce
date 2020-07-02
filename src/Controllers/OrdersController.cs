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
        public IActionResult Index()
        {
            List<OrdersModel> banana = GetData();
            return View(banana);
        }

        private List<OrdersModel> GetData()
        {
            var list = new List<OrdersModel>();

            for (var i = 0; i < 10; i++)
            {
                list.Add(new OrdersModel
                {

                    nr = i + 1,
                    Id = 125478 + i,
                    Date = DateTime.Now.AddDays(i),
                   // .Now.AddDays(i),
                    Price = (decimal)1299.99 + (i * 2),
                    Qty = 1,
                    Status = i % 2 == 0
                }); ; ; ; ;
 
            }
                     return list;
            //---

        }
    }
}
