using System;

namespace dream_holiday.Models
{
    public class UserAccountModel
    {
        public Guid Id;

        public UserAccount UserAccount { get; set; }
        public ApplicationUser User { get; set; }
    }
}