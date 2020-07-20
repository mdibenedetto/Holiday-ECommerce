using System;
using System.Security.Claims;
using System.Threading.Tasks;
using dream_holiday.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace dream_holiday.Models.EntityServices
{
    public abstract class BaseEntityService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected IHttpContextAccessor _contextAccessor;

        public BaseEntityService(ApplicationDbContext context,
                                 UserManager<ApplicationUser> userManager,
                                 IHttpContextAccessor contextAccessor
                                 )
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        protected async Task<ApplicationUser> GetCurrentUser()
        {
            var currentUser = _contextAccessor.HttpContext.User;

            return await _userManager.GetUserAsync(currentUser);
        }
    }
}
