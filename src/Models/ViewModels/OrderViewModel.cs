using System;
namespace dream_holiday.Models.ViewModels
{
    /// <summary>
    /// This class allows to create a View Data Aggregation
    /// between Order, UserAccount and  ApplicationUser tables
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// isAdmin is a flag used to indentify if the user is an Admin,
        /// and display only what the Admin can see on the website
        /// </summary>
        public static bool isAdmin;

        public Order Order { get; internal set; }
        public UserAccount Customer { get; internal set; }
        public ApplicationUser User { get; internal set; }
    }
}
