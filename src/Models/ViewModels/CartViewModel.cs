using System;
namespace dream_holiday.Models.ViewModels
{
    /// <summary>
    /// This class allows to create a View Data Aggregation
    /// between Cart and TravelPackage tables
    /// </summary>
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public TravelPackage TravelPackage { get; set; }
    }
}
