using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using dream_holiday.Models.EntityServices;
using dream_holiday.Models.ViewModels;

namespace dream_holiday.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ILogger<HolidayController> _logger;
        private CartService _cartService;

        public CartController(ILogger<HolidayController> logger,
                                CartService cartService)
        {
            _logger = logger;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            List<CartViewModel> cartList = null;

            try
            {
                cartList = await _cartService.GetCartUser();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("CartController =>  Index", ex);
                throw ex;
            }

            return View(cartList);

        }

        public IActionResult delete(Guid? cartId)
        {
            try
            {
                _cartService.RemoveCartItem(cartId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("CartController => delete", ex);
                throw ex;
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
