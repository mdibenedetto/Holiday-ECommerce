using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using Microsoft.AspNetCore.Mvc;

namespace dream_holiday.Controllers
{

    [Route("checkout-cart")]
    public class CheckoutController : Controller
    {


        private readonly ApplicationDbContext _context;

        public CheckoutController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {

            this.MockData();

            var checkout = _context.Checkout
               .Where(c => c.Id == Guid.NewGuid())
               .FirstOrDefault();



            var mycart = GetData();
            return View(mycart);
        }


        private void MockData()
        {
            if (_context.Checkout.Any())
            {
                return;
            }

            //   var checkout = _context.Checkout
            //     .Where(c => c.Id == Guid.NewGuid())
            //   .FirstOrDefault();

            var checkout = new Checkout();

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

        private Checkout GetData()
        {
            var mycart = new Checkout();
           
            mycart.FirstName = "John";
            mycart.LastName = "Banana";
            mycart.Email = "banana@gmail.com";

            mycart.Address = "56A Travel Street";
            mycart.Address2 = "Green Avenue";


            mycart.Country = "Ireland";
            mycart.City = "Dublin";
            mycart.EirCode = "D02 4A8B";


            mycart.NameOnCard = "John Banana";

            mycart.CardNumber = "1254 7854 9658";

            mycart.Expiration = DateTime.Now.AddYears(3);
      
            mycart.CVC = "236";

            mycart.FirstItem = "Croatia: Blue Lagoon";
            mycart.SecondItem = "Italy: BolognaStreets";
            mycart.ThirdItem = "France: Eating Croissants Tour";



            return mycart;
                
        }
    }


}

