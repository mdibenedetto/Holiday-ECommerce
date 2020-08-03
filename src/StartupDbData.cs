using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Data;
using dream_holiday.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using ILogger = Serilog.ILogger;

namespace dream_holiday
{
    public class StartupDbData
    {
        internal static void SeedData(ApplicationDbContext _context)
        {
            bool forceClean = false;
            try
            {               

                if (forceClean)
                {
                    _context.OrderDetail.Clear();
                    _context.Cart.Clear();

                    _context.TravelPackage.Clear();
                    _context.SaveChanges();
                }


                if (_context.TravelPackage.Any())
                {
                    return;
                }

                var list = buildList();
                _context.TravelPackage.AddRange(list);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex, "An error occurred while seeding the database.");
            }
        }


        static List<TravelPackage> buildList()
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

