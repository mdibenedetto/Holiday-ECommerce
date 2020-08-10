using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using dream_holiday.Data;
using dream_holiday.Models;

namespace dream_holiday.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AdminController(ApplicationDbContext context,
                                 UserManager<ApplicationUser> userManager,
                                RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /Admin
        public async Task<IActionResult> Index()
        {

            var users = await _context
                      .ApplicationUser
                      .Select(u => new
                      {
                          User = u,
                          IsAdmin = _context.UserRoles.Count(ur => ur.UserId == u.Id) > 0
                      })
                    .ToListAsync();

            var list = users.Select(u => (u.User, u.IsAdmin)).ToList();

            return View(list);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var applicationUser = await _context.ApplicationUser.FindAsync(id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Upgrade/5
        [HttpPost, ActionName("Upgrade")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upgrade(Guid id)
        {
            await ToggleAdminRole(id, true);
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Downgrade/5
        [HttpPost, ActionName("Downgrade")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Downgrade(Guid id)
        {
            await ToggleAdminRole(id, false);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// This method toggles user's role from Admin to Not admin user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        async private Task ToggleAdminRole(Guid id, bool isAdmin)
        {
            var applicationUser = await _context.ApplicationUser.FindAsync(id);

            if (isAdmin)
            {
                _userManager.AddToRoleAsync(applicationUser, Roles.ADMIN).Wait();
            }
            else
            {
                _userManager.RemoveFromRoleAsync(applicationUser, Roles.ADMIN).Wait();
            }

            await _context.SaveChangesAsync();
        }

        private bool ApplicationUserExists(Guid id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
