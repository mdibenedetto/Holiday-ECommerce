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
    

        public IActionResult Index()
        {
            List<OrderDetailsModel> banana = GetData();
            return View(banana);
        }

        private List<OrderDetailsModel> GetData()
        {


            var list = new List<OrderDetailsModel>();

            for (var i = 0; i < 6; i++)
            {
                list.Add(new OrderDetailsModel
                {

                    nr = i + 1,
                    title = "Croatia Blue Lagoon",
                    description = "(Our half day tour from Split we take you to the most famous and the most popular Blue Lagoon in Croatia.)", 
                    Qty = 1,
                    Price = (decimal)298 + (i * 2),
                }); ; ; ; ;

            }
            return list;


            /// end deta data        
        }

        ////---
    }
}
