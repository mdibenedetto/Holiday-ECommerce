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
    /// <summary>
    /// This class handles all task related to the table TravelPackage.
    /// </summary>
    public class TravelPackageService : BaseService
    {

        public TravelPackageService(ApplicationDbContext context, UserResolverService userService)
            : base(context, userService) { }

        /// <summary>
        /// This method find all Travel Packages available into the database.
        /// It also filter them accordingly to filter chosen by the user
        /// </summary>
        /// <param name="destinations"></param>
        /// <param name="categories"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<List<TravelPackageViewModel>> FindAllTravelPackagesAsync(
                        string[] destinations, int[] categories, decimal price)
        {
            var list = await this.FindAllUserTravelPackagesAsync();          

            // filter by destination
            if (destinations != null && destinations.Length > 0)
            {
                list = list
                    .Where(tp => destinations.Contains(tp.TravelPackage.Country))
                    .ToList();
            }
            // filter by category
            if (categories != null && categories.Length > 0)
            {
                list = list
                    .Where(tp => categories.Contains(tp.TravelPackage.CategoryId))
                    .ToList();
            }
            // filter by price
            if (price > 0)
            {
                list = list.Where(tp => tp.TravelPackage.Price <= price).ToList();
            }

            return list;
        }
        /// <summary>
        /// This methos returns all travel packages the users have inserted in their cart.
        /// It also aggregates data to provides feedback of how the items are placed into the cart.
        /// This information are useful in the page Holiday.
        /// </summary>
        /// <returns></returns>
        public async Task<List<TravelPackageViewModel>> FindAllUserTravelPackagesAsync()
        {

            var user = await base.GetCurrentUserAsync();
            // First case - User NOT Logged In
            if (user == null)
            {
                return _context.TravelPackage
                    .Where(tp => tp.IsInstock == true)
                    .Select(tp => new TravelPackageViewModel
                    {
                        TravelPackage = tp,
                        TotalInCart = 0
                    }).ToList();
            }
            // Second case - User Logged In
            var _userAccountService = new UserAccountService(_context, _userService);
            var userAccount = await _userAccountService.GetCurrentUserAccountAsync();
            // userAccount == null if the current user is an Admin
            var query = (from tp in _context.TravelPackage
                         where tp.IsInstock == true
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
        /// <summary>
        /// This method return all list of categories which have been assigned to a Travel package.
        /// If a category has not been assigned yet to any package it will not be returned
        /// </summary>
        /// <returns></returns>
        internal List<Category> GetAssignedCategories()
        {
            // SELECT * FROM Category
            // WHERE CategoryID IN
            // (SELECT CategoryID FROM TravelPackage)
            return _context.Category
                .Where(cat => _context
                                .TravelPackage
                                .Select(tp => tp.CategoryId)
                                .Contains(cat.Id)
                              )
                .ToList();
        }
        /// <summary>
        /// This method returns the list of the all contry names assigned to a Travel package.
        /// </summary>
        /// <returns></returns>
        public List<string> GetTravelCountryNames()
        {
            return _context.TravelPackage
                .Where(tp => !String.IsNullOrEmpty(tp.Country))
                  .Select(tp => tp.Country)
                  .ToList();
        }
        /// <summary>
        /// This method finds a travel package by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TravelPackage Find(int id)
        {
            return _context.TravelPackage.Find(id);
        }

    }
}