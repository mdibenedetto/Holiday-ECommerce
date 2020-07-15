using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace dream_holiday.Controllers
{
    public class OrdersController : Controller
    {

        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id, String Status)
        {
            // todo: remove it when cart is ready
            this.MockData();

            //here is list
            List<Order> oders;

            //if id is not 
            if (id > 0)
            {
                oders = _context.Order
                    .Where(item => item.Id == id)
                    .ToList();
            }
            //if the status is not empty
            else if (!String.IsNullOrEmpty(Status))
                {
                oders = _context.Order
                    //here filter by Status 
                     .Where(item => item.Status.Equals(Status))       
                       .ToList();
            }
            else
            {
                oders = _context.Order.ToList();
            }

            //List<Order> oders =  GetData(id);

            //GetData(Status);        

            return View(oders);           
        }

        private void MockData()
        {
            if (_context.Order.Any())
            {
                return;
            }

            var list = new List<Order>();

            for (var i = 0; i < 10; i++)
            {
                list.Add(new Order
                {
                    Id = 12547 + i,
                    Date = DateTime.Now.AddDays(i),
                    // .Now.AddDays(i),
                    Price = (decimal)1299.99 + (i * 2),
                    Qty = i + 2,
                    Status = i % 2 == 0
                });
            }

            _context.Order.AddRange(list);
            _context.SaveChanges();
        }



        //filterring by ID
        private List<Order> GetData(int id)
        {
            var list = new List<Order>();

            for (var i = 0; i < 10; i++)
            {
                list.Add(new Order
                {                    
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
                // applied filter for id
                return list.FindAll(item => item.Id == id);
            }


        }

        // filtering by Status
        private List<Order> GetData(String Status)
        {
            var list = new List<Order>();

            for (var i = 0; i < 10; i++)
            {
                list.Add(new Order
                {
                  
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
                return list.FindAll(item => item.Equals(Status.ToString()));
            }

            

        }
    }
}
