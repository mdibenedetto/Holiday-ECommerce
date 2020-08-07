using System;
using System.Collections.Generic;

namespace dream_holiday.Models.ViewModels
{
    public class HolidayViewModel
    {
        public List<TravelPackageViewModel> HolidayItems;

        public List<TravelPackage> TravelPackages { get; set; }
        public List<String> CountryNames { get; set; }
        public List<Category> Categories { get; set; }
    }
}
