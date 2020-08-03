using System;
using System.Security.Claims;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using System.Linq;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using dream_holiday.Models.ViewModels;

namespace dream_holiday.Models.EntityServices
{
    public class UserAccountService : BaseService
    {

        public UserAccountService(ApplicationDbContext context,
            UserResolverService userService)
            : base(context, userService)
        {

        }


        async public Task<UserAccount> GetCurrentUserAccountAsync()
        {
            var user = await base.GetCurrentUserAsync();

            var _userAccount = (from u in _context.Users
                                where u.Id == user.Id
                                join ua in _context.UserAccount
                                on user.Id equals ua.User.Id
                                select ua)
                                .FirstOrDefault();

            return _userAccount ?? new UserAccount();
        }

        public UserAccountViewModel findUserAccount(Guid userId)
        {
            // select data from table  Users (ApplicationUser)
            // and left join with table UserAccount
            var query = (from user in _context.Users
                         where user.Id == userId
                         join ua in _context.UserAccount
                                on user.Id equals ua.User.Id
                                into userAccount_join
                         from _userAccount in userAccount_join.DefaultIfEmpty()
                         select new UserAccountViewModel
                         {
                             Id = _userAccount.Id,
                             UserAccount = _userAccount,
                             User = user
                         });
            // take the first row
            UserAccountViewModel userAccount = query.FirstOrDefault();

            return userAccount;
        }


        public async Task<ApplicationUser> updateUserAsync(UserAccount userAccount)
        {
            var user = await _userManager.FindByIdAsync(userAccount.User.Id.ToString());
            user.UserName = userAccount.User.UserName;
            user.Email = userAccount.User.Email;

            if (user.PasswordHash != userAccount.Password)
            {
                user.PasswordHash = _userManager
                  .PasswordHasher
                  .HashPassword(user, userAccount.Password);

                // update only those fields which has changed
                await _userManager.UpdateAsync(user);
            }

            return user;
        }

        public async Task<UserAccount> updateUserAccount(UserAccount userAccount, ApplicationUser user)
        {
            var newUserAccount = new UserAccount();

            newUserAccount.User = user;
            newUserAccount.Id = userAccount.Id;
            newUserAccount.Title = userAccount.Title;
            newUserAccount.FirstName = userAccount.FirstName;
            newUserAccount.LastName = userAccount.LastName;

            try
            {
                newUserAccount.BirthDate = new DateTime(
                    userAccount.BirthYear,
                    userAccount.BirthMonth,
                    userAccount.BirthDay);
            }
            catch
            {
                Log.Warning("Date format is wrong");
            }

            //newUserAccount.Password = userAccount.Password;
            //newUserAccount.RetypePassword = userAccount.RetypePassword;
            newUserAccount.Country = userAccount.Country;
            newUserAccount.Address = userAccount.Address;
            newUserAccount.Address2 = userAccount.Address2;
            newUserAccount.City = userAccount.City;
            newUserAccount.County = userAccount.County;
            newUserAccount.Telephone = userAccount.Telephone;
            newUserAccount.CardHolderFullName = userAccount.CardHolderFullName;
            newUserAccount.CardNumber = userAccount.CardNumber;
            newUserAccount.CardCVC = userAccount.CardCVC;
            newUserAccount.CardMonth = userAccount.CardMonth;
            newUserAccount.CardYear = userAccount.CardYear;
            newUserAccount.CountryBilling = userAccount.CountryBilling;
            newUserAccount.AddressBilling = userAccount.AddressBilling;
            newUserAccount.Address2Billing = userAccount.Address2Billing;
            newUserAccount.CityBilling = userAccount.CityBilling;
            newUserAccount.County2Billing = userAccount.County2Billing;

            if (UserAccountModelExists(userAccount.Id))
            {
                _context.Update(newUserAccount);
            }
            else
            {
                _context.Add(newUserAccount);
            }
            await _context.SaveChangesAsync();

            return newUserAccount;
        }

        public bool UserAccountModelExists(Guid id)
        {
            return _context.UserAccount.Any(e => e.Id == id);
        }
    }
}
