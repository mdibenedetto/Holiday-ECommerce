using System;

namespace dream_holiday.Models.ViewModels
{
    public class UserAccountViewModel
    {
        public Guid Id;

        public UserAccount UserAccount { get; set; }
        public ApplicationUser User { get; set; }
    }
}