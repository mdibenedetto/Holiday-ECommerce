 
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
 
using dream_holiday.Models;
using dream_holiday.Models.EntityServices;

namespace dream_holiday.Controllers
{
    [Authorize]   
    public class CheckoutController : Controller
    {
        private readonly ILogger<CheckoutController> _logger;
        private readonly CheckoutService _checkoutService;

        public CheckoutController(ILogger<CheckoutController> logger, CheckoutService checkoutService)
        {
            _logger = logger;
            _checkoutService = checkoutService;
        }

        // GET: /Checkout
        public async Task<IActionResult> Index()
        {
            Checkout checkout = null;
            try
            {
                checkout = await _checkoutService.NewCheckoutAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("CheckoutController => Index", ex);
                throw ex;
            }

            return View(checkout);
        }

        // POST: /Checkout
        [HttpPost]        
        [ValidateAntiForgeryToken]
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
                _logger.LogError("CheckoutController => ProcessCheckout", ex);
                throw ex;
            }

        }

    }
}

