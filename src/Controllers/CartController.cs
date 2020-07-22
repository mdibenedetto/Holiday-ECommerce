using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace dream_holiday.Controllers
{
    [Authorize]
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

            var userAccount = await GetCurrentUser();

            var cartList = (from c in _context.Cart
                            where c.UserAccount.Id == userAccount.Id
                            join t in _context.TravelPackage
                            on c.TravelPackage.Id equals t.Id
                            select new CartViewModel { Cart= c, TravelPackage =  t })
                              .ToList();


            decimal price;
            decimal itemTotal;
            decimal subTotal = 0;
            int totalQty = 0;

            foreach (var item in cartList)
            {
                var cart = item.Cart;

                price = cart.Price;
                itemTotal = cart.Qty * price;
                subTotal += itemTotal;
                totalQty += cart.Qty;                
            }
           
            ViewBag.TotalPrice = subTotal;
            ViewBag.Quantity = totalQty;

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

    }
}
