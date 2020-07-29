using System;
using System.Security.Claims;
using System.Threading.Tasks;
using dream_holiday.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace dream_holiday.Models.EntityServices
{
    public abstract class BaseService
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        protected IHttpContextAccessor _httpContext; 
        private UserResolverService _userService;

        //public BaseService(ApplicationDbContext context,
        //                         UserManager<ApplicationUser> userManager,
        //                         IHttpContextAccessor httpContext
        //                         )
        //{
        //    _context = context;
        //    _userManager = userManager;
        //    _httpContext = httpContext;
        //}

        protected BaseService(ApplicationDbContext context, UserResolverService userService)
        {
            _context = context;
            _userService = userService;
            _userManager = userService.getUserManager();
            _httpContext = userService.getHTTPContext(); 
        }

        protected async Task<ApplicationUser> GetCurrentUser()
        {
            var currentUser = _httpContext.HttpContext.User;

            return await _userManager.GetUserAsync(currentUser);
        }
    }
}
