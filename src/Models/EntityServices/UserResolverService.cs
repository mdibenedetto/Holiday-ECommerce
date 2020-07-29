using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace dream_holiday.Models.EntityServices
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public UserResolverService(IHttpContextAccessor httpContext,
                                    UserManager<ApplicationUser> userManager)
        {
            _httpContext = httpContext;
            _userManager = userManager;
        }

        public UserManager<ApplicationUser>  getUserManager()
        {
            return _userManager;
        }

        public IHttpContextAccessor getHTTPContext()
        {
            return _httpContext;
        }
         
    }
}
