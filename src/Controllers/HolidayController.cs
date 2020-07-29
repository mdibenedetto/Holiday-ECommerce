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
        private readonly ILogger<HolidayController> _logger;
        private readonly TravelPackageService _travelPackageService;
        private readonly CartService _cartService;

        public HolidayController(   ILogger<HolidayController> logger,
                                     TravelPackageService travelPackageService,
                                     CartService cartService
                                 )
        {           
            _logger = logger;
            _travelPackageService = travelPackageService;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var model = new HolidayViewModel();

            try
            {
                var list = _travelPackageService.findAllTravelPackages();
                ViewBag.holidayItems = list;


                model.TravelPackages = list;
                model.CountryNames = _travelPackageService.getTravelCountries();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Index", ex);
                throw ex;
            }

            return View(model);
        }

        public IActionResult Detail(int Id)
        {
            TravelPackage item;
            try
            {
                item = _travelPackageService.Find(Id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("Detail", ex);
                throw ex;
            }

            return View(item);
        }

        [HttpGet("api/travelpackages")]
        public JsonResult LoadTravelPackages(
            [FromQuery] String[] destinations,
            [FromQuery] Decimal price = 0)
        {
            List<TravelPackage> list = null;
            try
            {
                list = _travelPackageService.findAllTravelPackages(destinations, price);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("LoadTravelPackages", ex);
                throw ex;
            }

            return Json(list);
        }

        public IActionResult AddToCart(int tpId)
        {
            try
            {                
                _cartService.AddTravelPackageToCart(tpId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("AddToCart", ex);
                throw ex;
            }

            return RedirectToAction("Detail", new { Id = tpId });
        }

    }
}
