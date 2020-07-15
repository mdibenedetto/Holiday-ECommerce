using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dream_holiday.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            
            //here is list
            List<Cart> cartList;

            cartList = _context.Cart.ToList();

            return View(cartList);

           
        }



        //method MockData
        private void MockData()
        {
            if (_context.Cart.Any())
            {
                return;
            }

            var mockList = new List<Cart>();
            for (int i = 0; i < 8; i++)
            {
                mockList.Add(new Cart
                {
                    Title = "Holiday package name - " + (i + 1),
                    Description = "Lorem ipsum dolor sit amet " +
                    "consectetur adipisicing elit.Exercitationem," +
                    " aspernatur!",
                    IsInstock = true,
                    Price = 1003 + i,
                    Qty = 1 + i
                });
            }

            _context.Cart.AddRange(mockList);
            _context.SaveChanges();

        }



    }
}
