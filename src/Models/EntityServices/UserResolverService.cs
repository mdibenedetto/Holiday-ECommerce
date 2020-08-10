using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace dream_holiday.Models.EntityServices
{
    /// <summary>
    /// This class handles all task related the User session.
    /// </summary>
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

        /// <summary>
        /// This method return the object which handle basic Application User operation
        /// such Login, Hash code password and etc.
        /// </summary>
        /// <returns></returns>
        public UserManager<ApplicationUser> GetUserManager()
        {
            return _userManager;
        }

        /// <summary>
        /// This method return the object which allows to access the current application server session
        /// and its context.
        /// </summary>
        /// <returns></returns>
        public IHttpContextAccessor GetHTTPContext()
        {
            return _httpContext;
        }

    }
}
