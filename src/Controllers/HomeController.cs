using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dream_holiday.Data;
using dream_holiday.Models.ViewModels;
using dream_holiday.Models.EntityServices;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace dream_holiday.Controllers
{
    public class HomeController : Controller
    {    
        private readonly ILogger<HomeController> _logger;
        private readonly TravelPackageService _travelPackageService;

        public HomeController( TravelPackageService travelPackageService, ILogger<HomeController> logger)
        {
            _travelPackageService = travelPackageService;
            _logger = logger;
        }


        public async Task<IActionResult> IndexAsync()
        {

            var model = new HomeViewModel();

            try
            {
                model.HolidayItems = await _travelPackageService.FindAllUserTravelPackagesAsync();
              
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("HomeController => Index", ex); 
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
