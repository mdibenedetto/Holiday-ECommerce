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

            return View();
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
            var base_Image_Url = "./img/holIday";

            List<TravelPackage> holidayItems =
                new List<TravelPackage> {
                    new TravelPackage{
                        Image = base_Image_Url + "/barcelona.jpg",
                        Name = "Barcelona",
                        Price= 2000,
                        Description= "Nulla vitae elit libero, a pharetra augue mollis interdum.",
                    },
                    new TravelPackage  {

                        Image= base_Image_Url + "/moscow.jpg",
                        Name= "Moscow",
                        Price= 1600,
                        Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    },
                    new TravelPackage {

                        Image= base_Image_Url + "/thailand.jpg",
                        Name= "Thailand",
                        Price= 1000,
                        Description=
                                "Praesent commodo cursus magna, vel scelerisque nisl consectetur.",
                    },
                    new TravelPackage  {

                        Image= base_Image_Url + "/new_zealand.jpg",
                        Name= "New Zealand",
                        Price= 2000,
                        Description= "Nulla vitae elit libero, a pharetra augue mollis interdum.",
                    },
                    new TravelPackage  {

                        Image= base_Image_Url + "/goa.jpg",
                        Name= "Goa",
                        Price= 1600,
                        Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    },
                    new TravelPackage  {

                        Image= base_Image_Url + "/france.jpg",
                        Name= "France",
                        Price= 1000,
                        Description=
                                "Praesent commodo cursus magna, vel scelerisque nisl consectetur.",
                    },
                        new TravelPackage{

                        Image= base_Image_Url + "/canada.jpg",
                        Name= "Canada",
                        Price= 2000,
                        Description= "Nulla vitae elit libero, a pharetra augue mollis interdum.",
                    },
                    new TravelPackage  {

                        Image= base_Image_Url + "/turkey.jpg",
                        Name= "Turkey",
                        Price= 1600,
                        Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    },
                    new TravelPackage {

                        Image= base_Image_Url + "/egypt.jpg",
                        Name= "Egypt",
                        Price= 1000,
                        Description=
                                "Praesent commodo cursus magna, vel scelerisque nisl consectetur.",
                    },
                    new TravelPackage {

                        Image= base_Image_Url + "/japan.jpg",
                        Name= "Japan",
                        Price= 2000,
                        Description= "Nulla vitae elit libero, a pharetra augue mollis interdum.",
                    },
                    new TravelPackage {

                        Image= base_Image_Url + "/brazil.jpg",
                        Name= "Brazil",
                        Price= 1600,
                        Description= "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    }
                };
            return holidayItems;

        }
    }


}
