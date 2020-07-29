using System;
using System.Collections.Generic;
using dream_holiday.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace dream_holiday.Models.EntityServices
{
    public class TravelPackageService
    {
        private ApplicationDbContext _context;
        private UserResolverService _userService;

        public TravelPackageService(ApplicationDbContext context, UserResolverService userService)
        {
            _context = context;
            _userService = userService;
        }

        public List<TravelPackage> findAllTravelPackages(string[] destinations, decimal price)
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

            return list;
        }

        public List<TravelPackage> findAllTravelPackages()
        {
            return _context.TravelPackage.ToList();
        }

        public List<string> getTravelCountries()
        {
            return _context.TravelPackage
                .Where(tp => tp.Country != "")
                  .Select(tp => tp.Country)
                  .ToList();
        }

        public TravelPackage Find(int id)
        {
            return _context
                 .TravelPackage
                 .Find(id); 
        }
    }
}