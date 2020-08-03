using System;
using System.Collections.Generic;

namespace dream_holiday.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public static DateTime OrderDate { get; set; }
        public OrderDetail OrderDetail;
        public TravelPackage TravelPackage;
    }

}
