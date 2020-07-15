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
            // todo Rami:
            // 1. all user should comes from
            // checkout.UserAccount (model.UserAccount.Firstname and blab bla)
            // 2. when I click to checkout button (link) create the order.
            // 2.1: to create the order add Insert on table Order, OrderDetail
            // get the checkout data of the user; 
            this.MockData();

            var userAccount = await GetCurrentUser();           
            var checkout = _context
                .Checkout
               .Where(c => c.UserAccount.Id == userAccount.Id)
               .FirstOrDefault();
            
            return View(checkout);
        }

        // /checkout-cart/process
        private async Task<IActionResult> Process(Checkout checkout)
        {
            // Insert into table Order
            // insert into table orderDetail
            // saveChange()

            return RedirectToRoute("/");          
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

        async private void MockData()
        {
            if (_context.Checkout.Any())
            {
                return;
            }

            var userAccount = await GetCurrentUser();

            var checkout = new Checkout();

            checkout.UserAccount = userAccount;
            checkout.FirstName = "John";
            checkout.LastName = "Banana";
            checkout.Email = "banana@gmail.com";
            checkout.Address = "56A Travel Street";
            checkout.Address2 = "Green Avenue";
            checkout.Country = "Ireland";
            checkout.City = "Dublin";
            checkout.EirCode = "D02 4A8B";
            checkout.NameOnCard = "John Banana";
            checkout.CardNumber = "1254 7854 9658";
            checkout.Expiration = DateTime.Now.AddYears(3);
            checkout.CVC = "236";
            checkout.FirstItem = "Croatia: Blue Lagoon";
            checkout.SecondItem = "Italy: BolognaStreets";
            checkout.ThirdItem = "France: Eating Croissants Tour";

            _context.Checkout.AddRange(checkout);
            _context.SaveChanges();
        }

        
    }
}

