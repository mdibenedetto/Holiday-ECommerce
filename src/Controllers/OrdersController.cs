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
                     .Where(item => item.Status.Equals(Status))
                       .ToList();
            }
            else
            {
                oders = _context.Order.ToList();
            }     

            return View(oders);
        }


        public IActionResult Delete(int orderId)
        {
            var order = _context.Order.Find(orderId);
            _context.Order.Remove(order);

            var orderDetailList = _context.OrderDetail.Where(od => od.Order.Id == orderId);
            _context.OrderDetail.RemoveRange(orderDetailList);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new {
                id =0,
                Status = ""
            });
        }

        private void MockData()
        {
            if (_context.Order.Any())
            {
                return;
            }

            var list = new List<Order>();

            for (var i = 0; i < 2; i++)
            {
                list.Add(new Order
                {
                    Id = 12547 + i,
                    Date = DateTime.Now.AddDays(i),
                    // .Now.AddDays(i),
                    Price = (decimal)1299.99 + (i * 2),
                    Qty = i + 2,
                    Status = i % 2 == 0 ? "Approved" : "Not Approved"

                }); ; ;
            }

            _context.Order.AddRange(list);
            _context.SaveChanges();
        }

    }
}
