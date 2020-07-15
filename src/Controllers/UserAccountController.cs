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

namespace dream_holiday.Controllers
{
    [Route("user-account")]
    public class UserAccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager; 

        public UserAccountController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager 
            )
        {            
            _context = context;
            _userManager = userManager; 
        }


        private async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        // GET: UserAccount/Edit/5
        public IActionResult Index(Guid? userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var query = (from user in _context.Users
                         where user.Id == userId
                         join ua in _context.UserAccount
                                on user.Id equals ua.User.Id
                                into userAccount_join                      
                         from _userAccount in userAccount_join.DefaultIfEmpty()
                         select new UserAccountModel{
                            Id = _userAccount.Id,
                            UserAccount = _userAccount,
                            User = user
                         }) ;

            var userAccount = query.FirstOrDefault();
 
            return View(userAccount);
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

            // todo : fix view and switch back to ModelState.IsValid
            var IsValid = true; //ModelState.IsValid;
            if (IsValid)
            {
                try
                {
                    // update application user
                    var user = await _userManager.FindByIdAsync(userAccount.User.Id.ToString());
                    user.UserName = userAccount.User.UserName;
                    user.Email = userAccount.User.Email;
                    // update only those fields which changed

                    await _userManager.UpdateAsync(user);
                    //_context.Attach(user);

                    // update user account
                    newUserAccount.User = user;
                    newUserAccount.Id = userAccount.Id;
                    newUserAccount.FirstName = userAccount.FirstName;
                    newUserAccount.LastName = userAccount.LastName;
                    newUserAccount.BirthDay = userAccount.BirthDay;
                    newUserAccount.BirthMonth = userAccount.BirthMonth;
                    newUserAccount.BirthYear = userAccount.BirthYear;
                    newUserAccount.Password = userAccount.Password;
                    newUserAccount.RetypePassword = userAccount.RetypePassword;
                    newUserAccount.Country = userAccount.Country;
                    newUserAccount.Address = userAccount.Address;
                    newUserAccount.Address2 = userAccount.Address2;
                    newUserAccount.Town = userAccount.Town;
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
                    newUserAccount.TownBilling = userAccount.TownBilling;
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserAccountModelExists(userAccount.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),
                    new { userId = newUserAccount.User.Id }
                );
            }
            return RedirectToAction(nameof(Index),
                new { userId = newUserAccount.User.Id }
             );
        }

        private bool UserAccountModelExists(Guid id)
        {
            return _context.UserAccount.Any(e => e.Id == id);
        }

    }
}
