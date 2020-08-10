using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dream_holiday.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using dream_holiday.Models;
using dream_holiday.Models.EntityServices;

namespace dream_holiday.Controllers
{
    [Authorize] 
    public class UserAccountController : Controller
    {
        private readonly ILogger<UserAccountController> _logger;
        private readonly UserAccountService _userAccountService;

        public UserAccountController(
            ILogger<UserAccountController> logger,
            UserAccountService userAccountService)
        {

            _logger = logger;
            _userAccountService = userAccountService;
        }


        public IActionResult Index(Guid userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            try
            {
                var userAccount = _userAccountService.FindUserAccount(userId);
                return View(userAccount);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("UserAccountController => Index", ex);
                throw ex;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid userId, UserAccount userAccount)
        {
            var newUserAccount = new UserAccount();

            if (ModelState.IsValid)
            {
                try
                {
                    // =======================================================
                    // 1. update table ApplicationUser
                    // =======================================================
                    var user = await _userAccountService.UpdateUserAsync(userAccount);
                    // =======================================================
                    // 2. update table UserAccount
                    // =======================================================
                    newUserAccount = await _userAccountService.UpdateUserAccount(userAccount, user);

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError("UserAccountController => Edit", ex);

                    if (!_userAccountService.UserAccountModelExists(userAccount.Id))
                    {
                        return NotFound();
                    }

                    throw ex;
                }

            }

            return RedirectToAction(nameof(Index), new { userId = newUserAccount.User.Id });
        }
    }
}
