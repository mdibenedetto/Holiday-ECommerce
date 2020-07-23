using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using dream_holiday.Models;
using dream_holiday.Models.EntityServices;
using dream_holiday.Data;

namespace dream_holiday.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ApplicationDbContext _context;

        public LoginModel(
              ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                debugUserLogin();

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                // try first to find user  by email and password              

                var result = await _signInManager
                    .PasswordSignInAsync(Input.Email,
                                        Input.Password,
                                        Input.RememberMe,
                                        lockoutOnFailure: false);


                // if fails try to recover the associated email to that username
                if (!result.Succeeded)
                {
                    var foundUser = _userManager.FindByEmailAsync(Input.Email);
                    if (foundUser != null && foundUser.Result != null)
                    {
                        result = await _signInManager.PasswordSignInAsync(
                            foundUser.Result.UserName.Trim(),
                            Input.Password.Trim(),
                            Input.RememberMe,
                            lockoutOnFailure: false);
                    }

                    logUserStatus(foundUser, result);
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        async private void logUserStatus(
            Task<ApplicationUser> foundUser,
            Microsoft.AspNetCore.Identity.SignInResult result)
        {
            var user = foundUser.Result;
            var signinResult = result;
            if (signinResult.IsNotAllowed)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    _logger.LogWarning("Email isn't confirmed.");
                }

                if (!await _userManager.IsPhoneNumberConfirmedAsync(user))
                {
                    _logger.LogWarning("Phone Number isn't confirmed.");
                }
            }
            else if (signinResult.IsLockedOut)
            {
                _logger.LogWarning("Account is locked out.");
            }
            else if (signinResult.RequiresTwoFactor)
            {
                _logger.LogWarning("2FA required.");
            }
            else
            {
                _logger.LogWarning("Username or password is incorrect.");
                if (user == null)
                {
                    _logger.LogWarning("Username is incorrect.");
                }
                else
                {
                    _logger.LogWarning("Password is incorrect.");
                }
            }
        }

        private void debugUserLogin()
        {
            var appUser = new ApplicationUser();
            var hashPassword = _userManager
                        .PasswordHasher
                        .HashPassword(appUser, Input.Password);


            ApplicationUser resultFound = null;

            // username and passowrd 1
            resultFound = _context.Users.Where(u =>
            (u.UserName == Input.Email || u.Email == Input.Email)
            && u.PasswordHash == hashPassword
            ).FirstOrDefault();

            // username and passowrd 2
            resultFound = _context.Users.Where(u =>
            (u.UserName == Input.Email)
            ).FirstOrDefault();

            // username and passowrd 3
            resultFound = _context.Users.Where(u =>
            (u.Email == Input.Email)
            ).FirstOrDefault();

            // username and passowrd 4
            resultFound = _context.Users.Where(u =>
              u.PasswordHash == hashPassword
            ).FirstOrDefault();

        }
    }
}
