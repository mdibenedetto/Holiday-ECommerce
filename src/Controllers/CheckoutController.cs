using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Models;
using Microsoft.AspNetCore.Mvc;

namespace dream_holiday.Controllers
{

    [Route("checkout-cart")]
    public class CheckoutController : Controller
    {
       

        public IActionResult Index()
        {
            var mycart = GetData();
            return View(mycart);
        }


        private CheckoutModel GetData()
        {
            var mycart = new CheckoutModel();
           
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

