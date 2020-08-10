using System;
using System.Collections.Generic;

namespace dream_holiday.Models.ViewModels
{
    /// <summary>
    /// This class allows to create a View Data Aggregation
    /// between OrderDetail and TravelPackage tables
    /// </summary>
    public class OrderDetailViewModel
    {
        /// <summary>
        /// this field store the OrderDate which unique between order details
        /// </summary>
        public static DateTime OrderDate { get; set; }
        public OrderDetail OrderDetail;
        public TravelPackage TravelPackage;
    }
}
