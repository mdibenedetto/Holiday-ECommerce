using System;
using System.Collections.Generic;

namespace dream_holiday.Models.ViewModels
{
    /// <summary>
    /// This class allows to create a View Data Aggregation
    /// between Category and TravelPackage tables.
    /// It also used to provide a list of country names.
    /// </summary>
    public class HolidayViewModel
    {
        public List<TravelPackageViewModel> HolidayItems;

        public List<TravelPackage> TravelPackages { get; set; }
        public List<String> CountryNames { get; set; }
        public List<Category> Categories { get; set; }
    }
}
