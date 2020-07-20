using System;
using System.Collections.Generic;
using dream_holiday.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace dream_holiday.Models.EntityServices
{
    public class CartService : BaseEntityService
    {

        UserAccountService _userAccountManager;

        public CartService(ApplicationDbContext context,
                                 UserResolverService userService)
         : base(context, userService.getUserManager(), userService.getHTTPContext())
        {
            _userAccountManager = new UserAccountService(_context, userService);

        }

        public CartService(ApplicationDbContext context,
                     UserManager<ApplicationUser> userManager,
                      IHttpContextAccessor contextAccessor
                     )
      : base(context, userManager, contextAccessor)
        {
            _userAccountManager = new UserAccountService(_context, _userManager, contextAccessor);
        }


        public async Task<List<Cart>> GetCartUser()
        {
            var user = await _userAccountManager.GetCurrentUserAccountAsync();
            var cartList = _context.Cart
                                    .Join(_context.TravelPackage,
                                      c => c.TravelPackage.Id,
                                      tp => tp.Id,
                                      (cart, tavelpackage)=> cart
                                    )
                                    .Where(c => c.UserAccount.Id == user.Id)
                                    .ToList();
            return cartList;
        }

        async public void AddTravelPackageToCart(int productId)
        {

            var _userAccount = await _userAccountManager.GetCurrentUserAccountAsync();
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
