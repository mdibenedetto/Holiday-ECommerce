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
    /// <summary>
    /// This class handles all task related to the table Cart.
    /// </summary>
    public class CartService : BaseService
    {
        UserAccountService _userAccountManager;

        public CartService(ApplicationDbContext context, UserResolverService userService)
         : base(context, userService)
        {
            _userAccountManager = new UserAccountService(_context, userService);
        }

        /// <summary>
        /// This method retrieve the cart of the current logged in user. 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// This method add a travel package to the user's cart,
        /// if the package is already present into the cart it will increase the quantity 
        /// </summary>
        /// <param name="travelPackageId"></param>
        /// <returns></returns>
        async public Task<Cart> AddTravelPackageToCart(int travelPackageId)
        {
            Cart cartToUpdate = await FindCartItemByTravelPackageId(travelPackageId);

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

        /// <summary>
        /// This method remove a travel package from the user's cart,
        /// if the package quantity is more 0 it will decrease the quantity
        /// </summary>
        /// <param name="travelPackageId"></param>
        /// <returns></returns>
        async public Task<Cart> RemoveTravelPackageFromCart(int travelPackageId)
        {
            Cart cartToUpdate = await FindCartItemByTravelPackageId(travelPackageId);

            // if not exist
            if (cartToUpdate != null)
            {
                cartToUpdate.Qty--;

                if (cartToUpdate.Qty > 0)
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

        /// <summary>
        /// This method remove a travel package item from the user's cart.
        /// This method is more strict than RemoveTravelPackageFromCart()
        /// </summary>
        /// <param name="cartId"></param>
        async public void RemoveCartItem(Guid? cartId)
        {
            var cart = _context.Cart.Find(cartId);
            _context.Cart.Remove(cart);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// This method is used to retrive a cart item which needs to be either
        /// update or removed from the user's cart
        /// </summary>
        /// <param name="travelPackageId"></param>
        /// <returns></returns>
        async private Task<Cart> FindCartItemByTravelPackageId(int travelPackageId)
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
