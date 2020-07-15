using System;
using System.Threading.Tasks;
using dream_holiday.Models;
using Microsoft.AspNetCore.Mvc;

namespace dream_holiday.Controllers
{
    public partial class CartController
    {
        async public Task<IActionResult> AddToCart(int id)
        {
            var _userAccount = await GetCurrentUser();
            var _travelPackage = _context.TravelPackage.Find(id);

            _context.Cart.Add(new Cart
                {
                    TravelPackage = _travelPackage,
                    UserAccount = _userAccount,
                    Qty = 1,
                    Price = _travelPackage.Price
                }
            );

            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
