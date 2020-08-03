using System;
using System.Collections.Generic;
using dream_holiday.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Models.ViewModels;

namespace dream_holiday.Models.EntityServices
{
    public class CartService : BaseService
    {
        UserAccountService _userAccountManager;

        public CartService(ApplicationDbContext context, UserResolverService userService)
         : base(context, userService)
        {
            _userAccountManager = new UserAccountService(_context, userService);
        }

        public async Task<List<CartViewModel>> GetCartUser()
        {
            var user = await _userAccountManager.GetCurrentUserAccountAsync();
            var cartList = _context.Cart
                                    .Where(c => c.UserAccount.Id == user.Id)
                                    .Join(_context.TravelPackage,
                                      c => c.TravelPackage.Id,
                                      tp => tp.Id,
                                      (cart, tavelpackage) =>
                                        new CartViewModel { Cart = cart, TravelPackage = tavelpackage }
                                    )
                                    .ToList();
            return cartList;
        }

        async public Task<Cart> AddTravelPackageToCart(int travelPackageId)
        {

            //var _userAccount = await _userAccountManager.GetCurrentUserAccountAsync();
            //var _travelPackage = _context.TravelPackage.Find(travelPackageId);
            //Cart cartToUpdate;

            //cartToUpdate = _context.Cart
            //    .Where(cart =>
            //    cart.TravelPackage.Id == travelPackageId
            //    && cart.UserAccount.Id == _userAccount.Id)
            //    .FirstOrDefault();

            Cart cartToUpdate = await FindCartByTravelPackageId(travelPackageId);

            // if not exist
            if (cartToUpdate == null)
            {
                var _userAccount = await _userAccountManager.GetCurrentUserAccountAsync();
                var _travelPackage = _context.TravelPackage.Find(travelPackageId);

                cartToUpdate = new Cart
                {
                    UserAccount = _userAccount,
                    TravelPackage = _travelPackage,
                    Price = _travelPackage.Price,
                    Qty = 1
                };

                _context.Cart.Add(cartToUpdate);
            }
            else
            {
                cartToUpdate.Qty++;
                _context.Cart.Update(cartToUpdate);
            }

            _context.SaveChanges();

            return cartToUpdate;
        }

        async public Task<Cart> RemoveTravelPackageFromCart(int travelPackageId)
        { 
            Cart cartToUpdate = await FindCartByTravelPackageId(travelPackageId);
          

            // if not exist
            if (cartToUpdate != null)
            {
                cartToUpdate.Qty--;

                if (cartToUpdate.Qty >  0)
                {
                    _context.Cart.Update(cartToUpdate);                   
                   
                }
                else
                {                 
                    _context.Cart.Remove(cartToUpdate);
                }
            }

            _context.SaveChanges();
           
            return cartToUpdate;
        }

        async public void removeCart(Guid? cartId)
        {
            var cart = _context.Cart.Find(cartId);
            _context.Cart.Remove(cart);

            await _context.SaveChangesAsync();
        }

        async private Task <Cart> FindCartByTravelPackageId(int travelPackageId)
        {
            var _userAccount = await _userAccountManager     
                                    .GetCurrentUserAccountAsync();

            var _travelPackage = _context.TravelPackage.Find(travelPackageId);
                      

            var cartToUpdate = _context.Cart
                .Where(cart =>
                cart.TravelPackage.Id == travelPackageId
                && cart.UserAccount.Id == _userAccount.Id)
                .FirstOrDefault();

            return cartToUpdate;
        }
    }
}
