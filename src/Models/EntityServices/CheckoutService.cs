using System;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models.ViewModels;

namespace dream_holiday.Models.EntityServices
{
    /// <summary>
    /// This class handles all task related to the table Checkout.
    /// </summary>
    public class CheckoutService : BaseService
    {
        private readonly UserAccountService _userAccountManager;

        public CheckoutService(ApplicationDbContext context, UserResolverService userService)
        : base(context, userService)
        {
            _userAccountManager = new UserAccountService(_context, userService);
        }

        /// <summary>
        /// This method create a plain Checkout object with a reference to the current user.
        /// The return value is used to initialize the index page
        /// </summary>
        /// <param name="formCheckout"></param>
        /// <returns></returns>
        public async Task<Checkout> NewCheckoutAsync(Checkout formCheckout = null)
        {
            var userAccount = await _userAccountManager.GetCurrentUserAccountAsync();

            if (formCheckout != null)
            {
                formCheckout.UserAccount = userAccount;
                return formCheckout;
            }
            else
            {
                var checkout = new Checkout
                {
                    UserAccount = userAccount
                };
                return checkout;
            }
        }

        /// <summary>
        /// This method executes all step to process a cart checkout.
        /// It gets data from the cart, it computes the tolals (Price and Quantity),
        /// it makes the insert on the table Order and OrderDetail,
        /// it clear the user account cart.
        /// All of this query uses entity framework which is charge to look after the
        /// transaction/commit over the database
        /// </summary>
        /// <param name="formCheckout"></param>
        /// <returns></returns>
        public async Task ProcessCheckoutAsync(Checkout formCheckout)
        {
            //====================================================
            // Find the current user
            //====================================================
            var userAccount = await _userAccountManager.GetCurrentUserAccountAsync();

            //====================================================
            // get data from the cart of the current user;
            //====================================================            
            var cartList = _context.Cart
                            .Where(c => c.UserAccount.Id == userAccount.Id)
                            .Join(_context.TravelPackage,
                                c => c.TravelPackage.Id,
                                tp => tp.Id,
                                (cart, travelPackage) => new CartViewModel { Cart = cart, TravelPackage = travelPackage }
                            );

            // find the total price
            var totalPrice = cartList.ToList().Sum(c => c.Cart.Price);
            var totalItems = cartList.ToList().Sum(c => c.Cart.Qty);

            // Insert into table Order
            var newOrder = new Order
            {
                Checkout = formCheckout,
                Customer = userAccount,
                Date = DateTime.Today,
                Status = "Approved",
                Price = totalPrice,
                Qty = totalItems
            };

            //====================================================
            // Insert into table OrderDetails
            //====================================================
            var orderDetailId = 0;
            foreach (var item in cartList)
            {
                var cart = item.Cart;
                var travelPackage = item.TravelPackage;

                _context.OrderDetail.Add(new OrderDetail
                {
                    Order = newOrder,
                    Id = ++orderDetailId,
                    //OrderId = newOrder.Id,
                    OrderDate = DateTime.Today,
                    Price = cart.Price,
                    Qty = cart.Qty,
                    TravelPackage = travelPackage
                });
            }
            //====================================================
            var cartToRemoveList = cartList.Select(item => item.Cart);
            _context.Cart.RemoveRange(cartToRemoveList);
            //====================================================

            //====================================================
            _context.SaveChanges();
            //====================================================
        }
    }
}
