using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dream_holiday.Models;
using dream_holiday.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using dream_holiday.Models.EntityServices;
using Microsoft.AspNetCore.Authorization;

namespace dream_holiday.Controllers
{
    [Authorize]
    //[Route("user-account")]
    public class UserAccountController_old : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserAccountController> _logger;
        private readonly UserAccountService _userAccountService;
        private readonly UserResolverService _userResolver;

        public UserAccountController_old(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            ILogger<UserAccountController> logger,
            UserAccountService userAccountService,
            UserResolverService userResolver
            )
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
            _userAccountService = userAccountService;
            _userResolver = userResolver;
        }


        public IActionResult Index(Guid userId)
        {
            if (userId == null)
            {
                return NotFound();
            }
            //// select data from table  Users (ApplicationUser)
            //// and left join with table UserAccount
            //var query = (from user in _context.Users
            //             where user.Id == userId
            //             join ua in _context.UserAccount
            //                    on user.Id equals ua.User.Id
            //                    into userAccount_join
            //             from _userAccount in userAccount_join.DefaultIfEmpty()
            //             select new UserAccountModel
            //             {
            //                 Id = _userAccount.Id,
            //                 UserAccount = _userAccount,
            //                 User = user
            //             });
            //// take the first row
            //UserAccountModel userAccount = query.FirstOrDefault();

            try
            {
                var userAccount = _userAccountService.findUserAccount(userId);
                return View(userAccount);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Index", ex);
                throw ex;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid userId,
            UserAccount userAccount)
        {

            //if (userId != userAccount.Id)
            //{
            //    return NotFound();
            //}
            var newUserAccount = new UserAccount();
            // todo: fix the data model validation (
            var IsValid = ModelState.IsValid;
            //var IsValid = true;

            if (IsValid)
            {
                try
                {
                    // =======================================================
                    // 1. update table ApplicationUser
                    // =======================================================

                    var user = await _userAccountService.updateUserAsync(userAccount);

                    //var user = await _userManager.FindByIdAsync(userAccount.User.Id.ToString());
                    //user.UserName = userAccount.User.UserName;
                    //user.Email = userAccount.User.Email;

                    //if (user.PasswordHash != userAccount.Password)
                    //{
                    //    user.PasswordHash = _userManager
                    //      .PasswordHasher
                    //      .HashPassword(user, userAccount.Password);

                    //    // update only those fields which has changed
                    //    await _userManager.UpdateAsync(user);
                    //}

                    // =======================================================
                    // 2. update table UserAccount
                    // =======================================================
                    // we set the field User with the current user.
                    // the object "user" is found by using the line:


                    //newUserAccount.User = user;
                    //newUserAccount.Id = userAccount.Id;
                    //newUserAccount.Title = userAccount.Title;
                    //newUserAccount.FirstName = userAccount.FirstName;
                    //newUserAccount.LastName = userAccount.LastName;

                    newUserAccount = await _userAccountService.updateUserAccount(userAccount, user);

                    //try
                    //{
                    //    newUserAccount.BirthDate = new DateTime(
                    //        userAccount.BirthYear,
                    //        userAccount.BirthMonth,
                    //        userAccount.BirthDay);
                    //}
                    //catch
                    //{
                    //    _logger.LogWarning("Date format is wrong");
                    //}

                    ////newUserAccount.Password = userAccount.Password;
                    ////newUserAccount.RetypePassword = userAccount.RetypePassword;
                    //newUserAccount.Country = userAccount.Country;
                    //newUserAccount.Address = userAccount.Address;
                    //newUserAccount.Address2 = userAccount.Address2;
                    //newUserAccount.City = userAccount.City;
                    //newUserAccount.County = userAccount.County;
                    //newUserAccount.Telephone = userAccount.Telephone;
                    //newUserAccount.CardHolderFullName = userAccount.CardHolderFullName;
                    //newUserAccount.CardNumber = userAccount.CardNumber;
                    //newUserAccount.CardCVC = userAccount.CardCVC;
                    //newUserAccount.CardMonth = userAccount.CardMonth;
                    //newUserAccount.CardYear = userAccount.CardYear;
                    //newUserAccount.CountryBilling = userAccount.CountryBilling;
                    //newUserAccount.AddressBilling = userAccount.AddressBilling;
                    //newUserAccount.Address2Billing = userAccount.Address2Billing;
                    //newUserAccount.CityBilling = userAccount.CityBilling;
                    //newUserAccount.County2Billing = userAccount.County2Billing;

                    //if (UserAccountModelExists(userAccount.Id))
                    //{
                    //    _context.Update(newUserAccount);
                    //}
                    //else
                    //{
                    //    _context.Add(newUserAccount);
                    //}
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError("Edit", ex);

                    if (!_userAccountService.UserAccountModelExists(userAccount.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index), new { userId = newUserAccount.User.Id }  );
            }
            return RedirectToAction(nameof(Index),  new { userId = newUserAccount.User.Id } );
        }

        //private bool UserAccountModelExists(Guid id)
        //{
        //    return _context.UserAccount.Any(e => e.Id == id);
        //}

    }
}
