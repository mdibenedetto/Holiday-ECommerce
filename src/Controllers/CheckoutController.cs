using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace dream_holiday.Controllers
{
    
    [Route("checkoutcart")]
    public class CheckoutCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
