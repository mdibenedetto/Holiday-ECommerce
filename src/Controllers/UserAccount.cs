//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using dream_holiday.Data;
//using dream_holiday.Models;

//namespace dream_holiday
//{
//    public class UserAccount : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public UserAccount(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: UserAccount
//        public async Task<IActionResult> Index()
//        {
//            return View(await _context.UserAccountModel.ToListAsync());
//        }

//        // GET: UserAccount/Details/5
//        public async Task<IActionResult> Details(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var userAccountModel = await _context.UserAccountModel
//                .FirstOrDefaultAsync(m => m.ID == id);
//            if (userAccountModel == null)
//            {
//                return NotFound();
//            }

//            return View(userAccountModel);
//        }

//        // GET: UserAccount/Create
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: UserAccount/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Email,Pasword,RetypePasword,Country,Address,Address2,Town,County,Telephone,CardHolderFullName,CardNumber,CardCVC,CountryBilling,AddressBilling,Address2Billing,TownBilling,County2Billing")] ApplicationUserModel userAccountModel)
//        {
//            if (ModelState.IsValid)
//            {
//                userAccountModel.ID = Guid.NewGuid();
//                _context.Add(userAccountModel);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(userAccountModel);
//        }

//        // GET: UserAccount/Edit/5
//        public async Task<IActionResult> Edit(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var userAccountModel = await _context.UserAccountModel.FindAsync(id);
//            if (userAccountModel == null)
//            {
//                return NotFound();
//            }
//            return View(userAccountModel);
//        }

//        // POST: UserAccount/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(Guid id, [Bind("ID,FirstName,LastName,Email,Pasword,RetypePasword,Country,Address,Address2,Town,County,Telephone,CardHolderFullName,CardNumber,CardCVC,CountryBilling,AddressBilling,Address2Billing,TownBilling,County2Billing")] UserAccountModel userAccountModel)
//        {
//            if (id != userAccountModel.ID)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(userAccountModel);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UserAccountModelExists(userAccountModel.ID))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(userAccountModel);
//        }

//        // GET: UserAccount/Delete/5
//        public async Task<IActionResult> Delete(Guid? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var userAccountModel = await _context.UserAccountModel
//                .FirstOrDefaultAsync(m => m.ID == id);
//            if (userAccountModel == null)
//            {
//                return NotFound();
//            }

//            return View(userAccountModel);
//        }

//        // POST: UserAccount/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(Guid id)
//        {
//            var userAccountModel = await _context.UserAccountModel.FindAsync(id);
//            _context.UserAccountModel.Remove(userAccountModel);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool UserAccountModelExists(Guid id)
//        {
//            return _context.UserAccountModel.Any(e => e.ID == id);
//        }
//    }
//}
