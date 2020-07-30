using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using dream_holiday.Models.EntityServices;
using dream_holiday.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
                _logger.LogError("Index", ex);
                throw ex;
            }

            return View(cartList);

        }

        public IActionResult delete(Guid? cartId)
        {
            try
            {
                _cartService.removeCart(cartId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("delete", ex);
                throw ex;
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
