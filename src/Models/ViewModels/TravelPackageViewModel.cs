using System;
namespace dream_holiday.Models.ViewModels
{
    /// <summary>
    /// This class allows to create a View Data Aggregation
    /// between TravelPackage, Cart and  Category tables
    /// </summary>
    public class TravelPackageViewModel
    {
        public TravelPackage TravelPackage { get; set; }

        public int TotalInCart { get; set; }
        public string CategoryName { get; set; }
    }
}
