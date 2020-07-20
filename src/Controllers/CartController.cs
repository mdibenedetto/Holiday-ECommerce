using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
 
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dream_holiday.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(
            ApplicationDbContext context,
             UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            //todo: remove it  where c.UserAccountId = 38042e71-c925-4d1b-a5fc-3cb50bd16fa6
            //MockData();

            var userAccount = await GetCurrentUser();

            var cartList = (from c in _context.Cart
                            where c.UserAccount.Id == userAccount.Id
                            join t in _context.TravelPackage
                            on c.TravelPackage.Id equals t.Id
                            select new CartViewModel { Cart= c, TravelPackage =  t })
                              .ToList();

          
            //foreach(var cart in cartList)
            //{

            //}
            //ViewBag.TotalPrice = xxxx
            return View(cartList);

        }

        public async Task<IActionResult> delete(Guid? cartId)
        {
            var cart = _context.Cart.Find(cartId);
            _context.Cart.Remove(cart);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));             
        }

        async private Task<UserAccount> GetCurrentUser()
        {
            var user = await _userManager
                               .GetUserAsync(HttpContext.User);

            var _userAccount = (from u in _context.Users
                                where u.Id == user.Id
                                join ua in _context.UserAccount
                                on user.Id equals ua.User.Id
                                select ua)
                                .FirstOrDefault();

            return _userAccount;
        }

        //todo: remove it
        //method MockData
        //async private void MockData()
        //{
        //    if (_context.Cart.Any())
        //    {
        //        return;
        //    }

        //    var mockList = new List<Cart>();
        //    // find the current user
        //    var _userAccount = await GetCurrentUser();

        //    for (int i = 0; i < 8; i++)
        //    {
        //        var newTravelPackage = _context
        //            .TravelPackage.Find(i + 1);

        //        mockList.Add(new Cart
        //        {
        //            UserAccount = _userAccount,
        //            TravelPackage = newTravelPackage,
        //            Price = 1003 + i,
        //            Qty = 1 + i
        //        });
        //    }

        //    _context.Cart.AddRange(mockList);
        //    _context.SaveChanges();

        //}



    }
}
