using System;
using System.Collections.Generic;

namespace dream_holiday.Models.ViewModels
{
    /// <summary>
    /// This class provide a view model to the Holiday page.
    /// It does not have an actual DB table reppresentation,
    /// it is specifically used to aggregate information on the Holiday page
    /// </summary>
    public class HomeViewModel
    {
        public List<TravelPackageViewModel> HolidayItems; 
    }
}
