using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dream_holiday.Models;
using dream_holiday.Data;
using Microsoft.EntityFrameworkCore;

namespace dream_holiday.Controllers
{
    [Route("user-account")]
    public class UserAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserAccount/Edit/5
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            //todo: remove var userAccountModel2 = await _context.UserAccount.FindAsync(id);

            var query = from ua in _context.UserAccount 
                        join u in _context.Users on ua.User.Id equals u.Id 
                        where u.Id == id  
                        select ua ; 
                      
            
             var userAccount = query.FirstOrDefault();
 

            if (userAccount == null)
            {
                var user = await _context.Users.FindAsync(id);
                var newUserAccount = new UserAccount { User = user };
                return View(newUserAccount); 
            }
            return View(userAccount);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserAccount userAccount)
        {
            if (id != userAccount.Id)
            {
                return NotFound();
            }
            // todo : fix view and switch back to ModelState.IsValid
            var IsValid = true; //ModelState.IsValid;
            if (IsValid)
            {
                try
                {
                    if (UserAccountModelExists(userAccount.Id))
                    {
                        _context.Update(userAccount);
                    }
                    else                 
                    {
                        _context.Add(userAccount);
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
                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Index), userAccount);
        }

        private bool UserAccountModelExists(Guid id)
        {
            return _context.UserAccount.Any(e => e.Id == id);
        }

    }
}
