using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dream_holiday.Models;
using dream_holiday.Data;

namespace dream_holiday.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

     
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ApplicationDbContext context,
            ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;


            //var list1 = _context
            //   .OrderDetail
            //   .Join(
            //       _context.TravelPackage,
            //       od => od.TravelPackage.Id,
            //       tp => tp.Id,
            //       (od, tp) => od
            //   ) 
            //   .ToList();

            //var list2 = from od in _context.OrderDetail
            //            join tp in _context.TravelPackage
            //            on od.TravelPackage.Id equals tp.Id  
            //            select od; 
       

        }


        public IActionResult Index()
        {
            return View();
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
