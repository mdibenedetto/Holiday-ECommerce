using System;

namespace dream_holiday.Models.ViewModels
{
    /// <summary>
    /// This class allows to create a View Data Aggregation
    /// between UserAccount and ApplicationUser tables
    /// </summary>
    public class UserAccountViewModel
    {
        public Guid Id;

        public UserAccount UserAccount { get; set; }
        public ApplicationUser User { get; set; }
    }
}