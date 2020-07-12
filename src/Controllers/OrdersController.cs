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
        public IActionResult Index(int id, String Status)
        {
            List<Order> banana = GetData(id); GetData(Status);
        

            return View(banana);
           
        }

 
        private List<Order> GetData(int id)
        {
            var list = new List<Order>();

            for (var i = 0; i < 10; i++)
            {
                list.Add(new Order
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

         
          if (id == 0)
            {
                //returning all stuff
                return list;
            }
            else
            {
                // applied filter for status
                return list.FindAll(item => item.Id == id);
            }


        }


        private List<Order> GetData(String Status)
        {
            var list = new List<Order>();

            for (var i = 0; i < 10; i++)
            {
                list.Add(new Order
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

            if (Status == " ")
            {
                //returning all stuff
                return list;
            }
            else
            {
                // applied filter for status
                return list.FindAll(item => item.Status.Equals(Status));
            }

            

        }
    }
}
