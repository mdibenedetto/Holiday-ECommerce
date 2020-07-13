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
        public async Task<IActionResult> Index(Guid? userId)
        {
            if (userId == null)
            {
                return NotFound();
            }


            //todo: remove var userAccountModel2 = await _context.UserAccount.FindAsync(id);

            var query = from ua in _context.UserAccount 
                        join u in _context.Users on ua.User.Id equals u.Id
                        where u.Id == userId  
                        select ua ;

            //var query2 = from u in _context.Users
            //             join ua in _context.UserAccount on u.Id equals ua.User.Id
            //             where u.Id == userId
            //            select ua;

            var userAccount = query.FirstOrDefault();
 

            if (userAccount == null)
            { 
                var user = await _context.Users.FindAsync(userId);
                var newUserAccount = new UserAccount {
                    Id = Guid.NewGuid(),
                    User = new ApplicationUser() };
                return View(newUserAccount); 
            }

            if (userAccount.User == null)
            {
                userAccount.User = await _context.Users.FindAsync(userId);
            }
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
                  
                    newUserAccount.User = await _context.Users.FindAsync(userAccount.User.Id);
                    newUserAccount.Id = userAccount.Id;
                    newUserAccount.FirstName = userAccount.FirstName;
                    newUserAccount.LastName = userAccount.LastName;

                    //_context.Update(userAccount.User);

                    if (UserAccountModelExists(userAccount.Id))
                    { 
                        //_context.Update(user);
                        //_context.UserAccount.Attach(userAccount);
                        _context.Update(newUserAccount);
                    }
                    else                 
                    {
                        _context.UserAccount.Add(newUserAccount);
                        //_context.Attach(userAccount);
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
                return RedirectToAction(nameof(Index), new { userId = newUserAccount.User.Id });
            }
            return RedirectToAction(nameof(Index), new { userId = newUserAccount.User.Id });
            //return View("index/?id=" + userId, userAccount);
        }

        private bool UserAccountModelExists(Guid id)
        {
            return _context.UserAccount.Any(e => e.Id == id);
        }

    }
}
