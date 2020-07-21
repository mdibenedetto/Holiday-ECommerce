using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dream_holiday.Controllers
{

    [Route("checkout-cart")]
    public class CheckoutController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutController(
            ApplicationDbContext context,
             UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        async public Task<IActionResult> Index()
        {
            var userAccount = await GetCurrentUser();

            var checkout = new Checkout
            {
                UserAccount = userAccount
            };

            return View(checkout);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessCheckout(Checkout formCheckout)
        {
            //====================================================
            // Find the current user
            //====================================================
            var userAccount = await this.GetCurrentUser();

            //====================================================
            // get data from the cart of the current user;
            //====================================================
            var cartList = _context.Cart
                            .Where(c => c.UserAccount.Id == userAccount.Id)
                            .Join(_context.TravelPackage,
                                c => c.TravelPackage.Id,
                                tp => tp.Id,
                                (cart, travelPackage) => new CartViewModel { Cart = cart, TravelPackage = travelPackage }
                            );


            // find the total price
            var totalPrice = 0; // cartList.Sum(c => c.Price);
            var totalItems = 0; // cartList.Sum(c => c.Qty);

            // Insert into table Order
            var newOrder = new Order
            {
                Checkout = formCheckout,
                Customer = userAccount,
                Date = DateTime.Today,
                Status = "Approved",
                Price = totalPrice,
                Qty = totalItems
            };

            var result = _context.Order.Add(newOrder);
            _context.SaveChanges();
            //====================================================
            // Insert into table OrderDetails
            //====================================================
            var orderDetailId = 0;
            foreach (var item in cartList)
            {
                var cart = item.Cart;
                var travelPackage = item.TravelPackage;

                _context.OrderDetail.Add(new OrderDetail
                {
                    Id = ++orderDetailId,
                    OrderId = newOrder.Id,
                    OrderDate = DateTime.Today,
                    Price = cart.Price,
                    Qty = cart.Qty,
                    TravelPackage = travelPackage
                });
            }
            //====================================================
            var cartToRemoveList = cartList.Select(item => item.Cart);
            _context.Cart.RemoveRange(cartToRemoveList);
            //====================================================

            //====================================================
            _context.SaveChanges();
            //====================================================
            return RedirectToRoute("/holiday");
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

