using System;
using dream_holiday.Data;

namespace dream_holiday.Models.EntityServices
{
    public class CheckoutService:BaseService
    {
        private readonly UserAccountService _userAccountManager;

        public CheckoutService(ApplicationDbContext context, UserResolverService userService)
        : base(context, userService)
        {
            _userAccountManager = new UserAccountService(_context, userService);
        }

        public async System.Threading.Tasks.Task<Checkout> NewCheckoutAsync()
        {
            var userAccount = await _userAccountManager.GetCurrentUserAccountAsync();

            var checkout = new Checkout
            {
                UserAccount = userAccount
            };

            return checkout;
        }
    }
}
