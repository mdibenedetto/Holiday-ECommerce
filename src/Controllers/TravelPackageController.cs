using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using dream_holiday.Data;
using dream_holiday.Models;

namespace dream_holiday.Controllers
{
    [Authorize(Roles = Roles.ADMIN)]
    public class TravelPackageController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnv;
        
        public TravelPackageController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnv = environment;
        }

        // GET: TravelPackage
        public async Task<IActionResult> Index()
        {
            return View(await _context.TravelPackage.ToListAsync());
        }

        // GET: TravelPackage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // GET: TravelPackage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravelPackage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Qty,Price,Image")] TravelPackage travelPackage)
        {
            if (ModelState.IsValid)
            {
                travelPackage.Image = UploadImage(travelPackage);

                _context.Add(travelPackage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(travelPackage);
        }        
       
    // GET: TravelPackage/Edit/5
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }    

            var travelPackage = await _context.TravelPackage.FindAsync(id);
            if (travelPackage == null)
            {
                return NotFound();
            }
            return View(travelPackage);
        }

        // POST: TravelPackage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Qty,Price,Image,ImageFile")] TravelPackage travelPackage)
        {
            if (id != travelPackage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    travelPackage.Image = UploadImage(travelPackage);

                    _context.Update(travelPackage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelPackageExists(travelPackage.Id))
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
            return View(travelPackage);
        }

        // GET: TravelPackage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // POST: TravelPackage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelPackage = await _context.TravelPackage.FindAsync(id);
            _context.TravelPackage.Remove(travelPackage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelPackageExists(int id)
        {
            return _context.TravelPackage.Any(e => e.Id == id);
        }

        String UploadImage(TravelPackage model)
        {
            String uniqueFileName ="", filePath = "";
            const String IMAGE_FOLDER = "img/holiday";

            // Todo other validations on your model as needed
            if (model.ImageFile != null)
            {
                uniqueFileName = GetUniqueFileName(model.ImageFile.FileName);
                var uploads = Path.Combine(_hostEnv.WebRootPath, IMAGE_FOLDER);
                filePath = Path.Combine(uploads, uniqueFileName);

                model.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return "/" + Path.Combine(IMAGE_FOLDER, uniqueFileName); ;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

    }
}
