using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using dream_holiday.Models;
using dream_holiday.Models.EntityServices;
using dream_holiday.Models.ViewModels; 
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

        public HolidayController(ILogger<HolidayController> logger,
                                     TravelPackageService travelPackageService,
                                     CartService cartService
                                 )
        {
            _logger = logger;
            _travelPackageService = travelPackageService;
            _cartService = cartService;
        }

        //  GET: /Holiday
        public async Task<IActionResult> IndexAsync()
        {
            var model = new HolidayViewModel();

            try
            {
                var list = await _travelPackageService.FindAllUserTravelPackagesAsync();

                model.HolidayItems = list;
                model.TravelPackages = list.Select(t => t.TravelPackage).Distinct().ToList();
                model.CountryNames = _travelPackageService.GetTravelCountryNames();
                model.Categories = _travelPackageService.GetAssignedCategories();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("HolidayController => Index", ex);
                throw ex;
            }

            return View(model);
        }

        //  GET: /Holiday/Detail/{id}
        public IActionResult Detail(int Id)
        {
            TravelPackage item;
            try
            {
                item = _travelPackageService.Find(Id);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("HolidayController => Detail", ex);
                throw ex;
            }

            return View(item);
        }

        [HttpGet("api/travelpackages")]
        public async Task<JsonResult> LoadTravelPackagesAsync(
            [FromQuery] String[] destinations,
            [FromQuery] int[] categories,
            [FromQuery] Decimal price = 0)
        {
            List<TravelPackageViewModel> list = null;
            try
            {
                list = await _travelPackageService
                              .FindAllTravelPackagesAsync(destinations, categories, price);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("HolidayController => LoadTravelPackages", ex);
                throw ex;
            }

            return Json(list);
        }

        //  GET: /Holiday/AddToCart/{id}
        public IActionResult AddToCart(int tpId)
        {
            try
            {
                _ = _cartService.AddTravelPackageToCart(tpId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("AddToCart", ex);
                throw ex;
            }

            return RedirectToAction("HolidayController => Detail", new { Id = tpId });
        }


        [HttpPost("api/addtocart")]
        public async Task<JsonResult> ApiAddToCartAsync(int tpId)
        {
            try
            {
                var cart = await _cartService.AddTravelPackageToCart(tpId); 

                //travelPackage
                return new JsonResult(new
                {
                    successed = true,
                    cart
                });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("HolidayController => ApiAddToCart", ex);
                return new JsonResult(new { successed = false });
                throw ex;
            }
        }

        [HttpPost("api/remofromcart")]
        public async Task<JsonResult> ApiRemoveFromCartAsync(int tpId) 
        {
            try
            {
                var cart = await _cartService.RemoveTravelPackageFromCart(tpId); 

                //travelPackage
                return new JsonResult(new
                {
                    successed = true,
                    cart
                });
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError("HolidayController => ApiRemoveFromCartAsync", ex);
                return new JsonResult(new { successed = false });
                throw ex;
            }

        }

        [HttpGet("api/getcartsummary")]
        public PartialViewResult GetCartSummary()
        {
            return PartialView("~/Views/Cart/_CartSummary.cshtml");
        }

    }
}
