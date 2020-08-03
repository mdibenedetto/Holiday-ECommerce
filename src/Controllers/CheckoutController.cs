using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using dream_holiday.Models.EntityServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace dream_holiday.Controllers
{
    [Authorize]
    [Route("checkout-cart")]
    public class CheckoutController : Controller
    { 
     
        private readonly ILogger<CheckoutController> _logger;
        private readonly CheckoutService _checkoutService; 

        public CheckoutController(ILogger<CheckoutController> logger, CheckoutService checkoutService)
        {
            _logger = logger;
            _checkoutService = checkoutService;
        }

        public async Task<IActionResult> Index()
        {
            Checkout checkout = null;
            try
            {
                checkout = await _checkoutService.NewCheckoutAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Index", ex);
                throw ex;
            }
           
            return View(checkout);
        }

        [HttpPost]
        // todo: check why ValidateAntiForgeryToken triggers an error
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessCheckout(Checkout formCheckout)
        {
            try
            {
                var IsValid = ModelState.IsValid;
                if (IsValid == false)
                {
                    var newCheckout = await _checkoutService.NewCheckoutAsync(formCheckout);
                    return View("index", newCheckout);
                }

                await _checkoutService.ProcessCheckoutAsync(formCheckout);

                return RedirectToAction("index", "thankyou");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("ProcessCheckout", ex);
                throw ex;
            }
  
        } 

    }
}

