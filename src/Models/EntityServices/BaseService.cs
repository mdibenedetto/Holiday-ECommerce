using System;
using System.Security.Claims;
using System.Threading.Tasks;
using dream_holiday.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace dream_holiday.Models.EntityServices
{
    /// <summary>
    /// This is the base class used by the Service,
    /// it provide common functionality shared by all service subclasses
    /// </summary>
    public abstract class BaseService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected IHttpContextAccessor _httpContext;
        protected UserResolverService _userService;

        protected BaseService(ApplicationDbContext context, UserResolverService userService)
        {
            _context = context;
            _userService = userService;
            _userManager = userService.GetUserManager();
            _httpContext = userService.GetHTTPContext();
        }

        /// <summary>
        /// This method return the current user account logged in
        /// and who is in session. It return the user asynchronously
        /// <returns></returns>
        protected async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var currentUser = _httpContext.HttpContext.User;
            return await _userManager.GetUserAsync(currentUser);
        }

        /// <summary>
        /// This method return the current user account logged in
        /// and who is in session
        /// </summary>
        /// <returns></returns>
        protected ApplicationUser GetCurrentUser()
        {
            var currentUser = _httpContext.HttpContext.User;
            return _userManager.GetUserAsync(currentUser).Result;
        }
    }
}
