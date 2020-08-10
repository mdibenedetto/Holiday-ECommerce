using System;
using System.Threading.Tasks;
using dream_holiday.Data;
using System.Linq;
using Serilog;
using dream_holiday.Models.ViewModels;

namespace dream_holiday.Models.EntityServices
{
    /// <summary>
    /// This class handles all task related the tables ApplicationUser and UserAccount.
    /// </summary>
    public class UserAccountService : BaseService
    {

        public UserAccountService(ApplicationDbContext context,
            UserResolverService userService) : base(context, userService)
        { }

        /// <summary>
        /// This method return the current User account
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// This method an user account by its user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public UserAccountViewModel FindUserAccount(Guid userId)
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

        /// <summary>
        /// This method update the Application User's data
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> UpdateUserAsync(UserAccount userAccount)
        {
            var user = await _userManager.FindByIdAsync(userAccount.User.Id.ToString());
            user.UserName = userAccount.User.UserName;
            user.Email = userAccount.User.Email;

            // if password didn't change is not necessary to make any update
            // This IF allows to over come the issue of a password which is hash coded twuice
            if (user.PasswordHash != userAccount.Password)
            {
                user.PasswordHash = _userManager.PasswordHasher
                                                .HashPassword(user, userAccount.Password);
                // update only those fields which has changed
                await _userManager.UpdateAsync(user);
            }

            return user;
        }
        /// <summary>
        /// This method update the UserAccount's data
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<UserAccount> UpdateUserAccount(UserAccount userAccount, ApplicationUser user)
        {
            var newUserAccount = new UserAccount();

            newUserAccount.User = user;
            newUserAccount.Id = userAccount.Id;
            newUserAccount.Title = userAccount.Title;
            newUserAccount.FirstName = userAccount.FirstName;
            newUserAccount.LastName = userAccount.LastName;

            // this try/catch handles the case where the values to compose a data are not valid
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

            // this IF handles the case to discriminate if we are doing a insert or an update
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
        /// <summary>
        /// This method check id the user Exist
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UserAccountModelExists(Guid id)
        {
            return _context.UserAccount.Any(e => e.Id == id);
        }
    }
}
