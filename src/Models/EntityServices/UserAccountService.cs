using System;
using System.Security.Claims;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using System.Linq;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace dream_holiday.Models.EntityServices
{
    public class UserAccountService : BaseService
    {
        
        public UserAccountService(ApplicationDbContext context, UserResolverService userService)
            : base(context, userService)
        { }
         

        async public Task<UserAccount> GetCurrentUserAccountAsync()
        {
            var user =  await base.GetCurrentUser();

            var _userAccount = (from u in _context.Users
                                where u.Id == user.Id
                                join ua in _context.UserAccount
                                on user.Id equals ua.User.Id
                                select ua)
                                .FirstOrDefault();

            return _userAccount;
        }
    }
}
