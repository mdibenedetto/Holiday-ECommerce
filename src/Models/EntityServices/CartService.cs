using System;
using dream_holiday.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace dream_holiday.Models.EntityServices
{
    public class CartService : BaseEntityService
    {

        UserAccountService _userAccountManager;


        public CartService(ApplicationDbContext context,
                     UserManager<ApplicationUser> userManager,
                      IHttpContextAccessor contextAccessor
                     )
      : base(context, userManager, contextAccessor)
        {
           
              _userAccountManager = new UserAccountService(_context, _userManager, contextAccessor);

        }

        async public void AddTravelPackageToCart(int productId)
        {

            var _userAccount = await _userAccountManager.GetCurrentUserAccount();
            var _travelPackage = _context.TravelPackage.Find(productId);

            _context.Cart.Add(new Cart
            {
                TravelPackage = _travelPackage,
                UserAccount = _userAccount,
                Qty = 1,
                Price = _travelPackage.Price
            });

            _context.SaveChanges();
 


        }
    }
}
