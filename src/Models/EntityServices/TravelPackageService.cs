using System;
using System.Collections.Generic;
using dream_holiday.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Models.ViewModels;

namespace dream_holiday.Models.EntityServices
{
    public class TravelPackageService : BaseService
    {

        public TravelPackageService(
            ApplicationDbContext context,
            UserResolverService userService)
            : base(context, userService)
        {

        }

        public async Task<List<TravelPackageViewModel>> findAllTravelPackagesAsync(string[] destinations, decimal price)
        {
            var list = await this.findAllTravelPackagesAsync();

            if (destinations != null && destinations.Length > 0)
            {
                list = list
                    .Where(tp => destinations.Contains(tp.TravelPackage.Country))
                    .ToList();
            }

            if (price > 0)
            {
                list = list.Where(tp => tp.TravelPackage.Price <= price).ToList();
            }

            return list;
        }

        public async Task<List<TravelPackageViewModel>> findAllTravelPackagesAsync()
        {

            var user = await base.GetCurrentUser();
            // First case - User NOT Logged In
            if (user == null)
            {
                return _context.TravelPackage
                    .Select(tp => new TravelPackageViewModel
                    {
                        TravelPackage = tp,
                        TotalInCart = 0
                    }).ToList();
            }
            // Second case - User Logged In
            var _userAccountService = new UserAccountService(_context, _userService);
            var userAccount = await _userAccountService.GetCurrentUserAccountAsync();


            var query = (from tp in _context.TravelPackage
                         select new TravelPackageViewModel
                         {
                             TravelPackage = tp,
                             TotalInCart = (from c in _context.Cart
                                            where c.UserAccount.Id == userAccount.Id
                                                && c.TravelPackage.Id == tp.Id
                                            select c.Qty).Sum()
                         });

            return query.ToList();

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

        public object findAllTravelPackagesInCart()
        {
            return null;
        }
    }
}