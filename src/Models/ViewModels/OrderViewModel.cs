using System;
namespace dream_holiday.Models.ViewModels
{
    public class OrderViewModel
    {
        public static bool isAdmin; 

        public Order Order { get; internal set; }
        public UserAccount Customer { get; internal set; }
        public ApplicationUser User { get; internal set; }
    }
}
