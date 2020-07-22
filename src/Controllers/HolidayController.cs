using System;
using System.Collections.Generic;
using System.Linq;

using dream_holiday.Data;
using dream_holiday.Models;
using dream_holiday.Models.EntityServices;
using dream_holiday.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace dream_holiday.Controllers
{
    public class HolidayController : Controller
    {
        private readonly ApplicationDbContext _context;
        protected readonly UserManager<ApplicationUser> _userManager;
        private IHttpContextAccessor _contextAccessor; 
        private readonly ILogger<HolidayController> _logger;
 
        public HolidayController(ApplicationDbContext context,
                                 UserManager<ApplicationUser> userManager,
                                 IHttpContextAccessor contextAccessor,
                                     ILogger<HolidayController> logger
                                 )
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        public IActionResult Index()
        {           
            this.MockData(); 

            var list = _context.TravelPackage.ToList();
            ViewBag.holidayItems = list;

            var model = new HolidayViewModel();
            model.TravelPackages = _context.TravelPackage.ToList();
            model.CountryNames = model.TravelPackages.Where(tp=> tp.Country != "").Select(tp => tp.Country).ToList();
            return View(model);
        }

        public IActionResult Detail(int Id)
        {
            var item = _context
                .TravelPackage
                .Find(Id);

            return View(item);
        }

        [HttpGet("api/travelpackages")]
        public JsonResult LoadTravelPackages(
            [FromQuery] String[] destinations,
            [FromQuery] Decimal price = 0)
        {
            var list = _context.TravelPackage.ToList();

            if (destinations != null && destinations.Length > 0)
            {
                list = list
                    .Where(tp => destinations.Contains(tp.Country))
                    .ToList();
            }

            if (price > 0)
            {
                list = list.Where(tp => tp.Price <= price).ToList();
            }

            return Json(list);
        }

        public IActionResult AddToCart(int tpId)
        {
            try
            {
                var cartService = new CartService(_context, _userManager, _contextAccessor);
                cartService.AddTravelPackageToCart(tpId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("AddToCart", ex);
                throw ex;
            }

            return RedirectToAction("Detail", new { Id = tpId });
        }

        private void MockData()
        {
            //_context.OrderDetail.Clear();
            //_context.Cart.Clear();
            //_context.TravelPackage.Clear();

            //_context.SaveChanges();
            if (_context.TravelPackage.Any())
            {
                return;
            }

            var list = buildList();
            _context.TravelPackage.AddRange(list);
            _context.SaveChanges();
        }


        List<TravelPackage> buildList()
        {
            var LOREM_IPSUM = @"                 
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut
                enim ad minim veniam, quis nostrud exercitation ullamco laboris
                nisi ut aliquip ex ea commodo consequat.

                Sed ut perspiciatis unde omnis iste natus error sit voluptatem
                accusantium doloremque laudantium, totam rem aperiam, eaque ipsa
                quae ab illo inventore veritatis et quasi architecto beatae vitae
                dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit
                aspernatur aut odit aut fugit, sed quia consequuntur";

            var base_Image_Url = "/img/holIday";

            List<TravelPackage> holidayItems =
                new List<TravelPackage> {
                    new TravelPackage{
                        IsInstock = true,
                        Image = base_Image_Url + "/barcelona.jpg",
                        Country = "Spain",
                        City = "Barcelona",
                        Name = "Barcelona",
                        Price= 2000,
                        Description= LOREM_IPSUM,
                    },
                    new TravelPackage  {
                           IsInstock = true,
                        Image= base_Image_Url + "/moscow.jpg",
                        Country= "Russia",
                        City= "Moscow",
                        Name= "Moscow",
                        Price= 1600,
                        Description= LOREM_IPSUM,
                    },
                    new TravelPackage {
                           IsInstock = true,
                        Image= base_Image_Url + "/thailand.jpg",
                        Country = "Thailand",
                        City = "Bangkok",
                        Name= "Thailand",
                        Price= 1000,
                        Description= LOREM_IPSUM,
                    },
                    new TravelPackage  {
                           IsInstock = true,
                        Image= base_Image_Url + "/new_zealand.jpg",
                        City= "New Zealand",
                        Name= "New Zealand",
                        Price= 2000,
                        Description= LOREM_IPSUM,
                    },
                    new TravelPackage  {
                           IsInstock = true,
                        Image= base_Image_Url + "/goa.jpg",
                        Country= "India",
                        City= "Goa",
                        Name= "Goa",
                        Price= 1600,
                        Description= LOREM_IPSUM,
                    },
                    new TravelPackage  {
                           IsInstock = true,
                        Image= base_Image_Url + "/france.jpg",
                        Country= "France",
                        City= "Paris",
                        Name= "France",
                        Price= 1000,
                        Description=LOREM_IPSUM,
                    },
                    new TravelPackage{
                           IsInstock = true,
                            Image= base_Image_Url + "/canada.jpg",
                            Country= "Canada",
                            City = "Niagra",
                            Name= "Canada",
                            Price= 2000,
                            Description= LOREM_IPSUM,
                    },
                    new TravelPackage  {
                           IsInstock = true,
                            Image= base_Image_Url + "/turkey.jpg",
                            Country= "Turkey",
                            City= "Istanbul",
                            Name= "Turkey",
                            Price= 1600,
                            Description= LOREM_IPSUM,
                    },
                    new TravelPackage {
                           IsInstock = true,
                        Image= base_Image_Url + "/egypt.jpg",
                        Country= "Egypt",
                        City= "Cairo",
                        Name= "Egypt",
                        Price= 1000,
                        Description= LOREM_IPSUM,
                    },
                    new TravelPackage {
                           IsInstock = true,
                        Image= base_Image_Url + "/japan.jpg",
                        Country= "Japan",
                        City= "Kioto",
                        Name= "Japan",
                        Price= 2000,
                        Description= LOREM_IPSUM,
                    },
                    new TravelPackage {
                           IsInstock = true,
                        Image= base_Image_Url + "/brazil.jpg",
                        Country= "Brazil",
                        City= "San Paolo",
                        Name= "Brazil",
                        Price= 1600,
                        Description= LOREM_IPSUM,
                    }
                };
            return holidayItems;

        }
    }


}
