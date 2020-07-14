using System;
using System.Collections.Generic;
using System.Linq;

using dream_holiday.Data;
using dream_holiday.Models;

using Microsoft.AspNetCore.Mvc;


namespace dream_holIday.Controllers
{
    public class HolIdayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HolIdayController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // todo= remove it when cart is ready
            this.MockData();

            var list = _context.TravelPackage.ToList();
            ViewBag.holidayItems = list;

            return View();
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

        private void MockData()
        {
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
            var base_Image_Url = "/img/holIday";

            List<TravelPackage> holidayItems =
                new List<TravelPackage> {
                    new TravelPackage{
                        Image = base_Image_Url + "/barcelona.jpg",
                        Country = "Spain",
                        City = "Barcelona",
                        Name = "Barcelona",
                        Price= 2000,
                        Description= "Nulla vitae elit libero, a pharetra augue mollis interdum.",
                    },
                    new TravelPackage  {
                        Image= base_Image_Url + "/moscow.jpg",
                        Country= "Russia",
                        City= "Moscow",
                        Name= "Moscow",
                        Price= 1600,
                        Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    },
                    new TravelPackage {

                        Image= base_Image_Url + "/thailand.jpg",
                        Country = "Thailand",
                        City = "Bangkok",
                        Name= "Thailand",
                        Price= 1000,
                        Description=
                                "Praesent commodo cursus magna, vel scelerisque nisl consectetur.",
                    },
                    new TravelPackage  {

                        Image= base_Image_Url + "/new_zealand.jpg",
                        City= "New Zealand",
                        Name= "New Zealand",
                        Price= 2000,
                        Description= "Nulla vitae elit libero, a pharetra augue mollis interdum.",
                    },
                    new TravelPackage  {

                        Image= base_Image_Url + "/goa.jpg",
                        Country= "India",
                        City= "Goa",
                        Name= "Goa",
                        Price= 1600,
                        Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    },
                    new TravelPackage  {
                        Image= base_Image_Url + "/france.jpg",
                        Country= "France",
                        City= "Paris",
                        Name= "France",
                        Price= 1000,
                        Description=
                                "Praesent commodo cursus magna, vel scelerisque nisl consectetur.",
                    },
                    new TravelPackage{
                            Image= base_Image_Url + "/canada.jpg",
                            Country= "Canada",
                            City = "Niagra",
                            Name= "Canada",
                            Price= 2000,
                            Description= "Nulla vitae elit libero, a pharetra augue mollis interdum.",
                    },
                    new TravelPackage  {
                            Image= base_Image_Url + "/turkey.jpg",
                            Country= "Turkey",
                            City= "Istanbul",
                            Name= "Turkey",
                            Price= 1600,
                            Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    },
                    new TravelPackage {
                        Image= base_Image_Url + "/egypt.jpg",
                        Country= "Egypt",
                        City= "Cairo",
                        Name= "Egypt",
                        Price= 1000,
                        Description= "Praesent commodo cursus magna, vel scelerisque nisl consectetur.",
                    },
                    new TravelPackage {

                        Image= base_Image_Url + "/japan.jpg",
                        Country= "Japan",
                        City= "Kioto",
                        Name= "Japan",
                        Price= 2000,
                        Description= "Nulla vitae elit libero, a pharetra augue mollis interdum.",
                    },
                    new TravelPackage {

                        Image= base_Image_Url + "/brazil.jpg",
                        Country= "Brazil",
                        City= "San Paolo",
                        Name= "Brazil",
                        Price= 1600,
                        Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    }
                };
            return holidayItems;

        }
    }


}
