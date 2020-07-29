using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dream_holiday.Models
{
    public class UserAccount
    { 
        public virtual ApplicationUser User { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public String Title { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public DateTime BirthDate { get; set; }

        //todo remove this field
        [NotMapped]
        public int BirthDay { get; set; }

        //todo remove this field
        [NotMapped]
        public int BirthMonth { get; set; }

        //todo remove this field
        [NotMapped]       
        public int BirthYear { get; set; }

        //todo remove this field
        [NotMapped]
        public String Password { get; set; }

        //todo remove this field
        [NotMapped]  
        public String RetypePassword { get; set; }

        public String Country { get; set; }

        public String Address { get; set; }

        public String Address2 { get; set; }

        public String Town { get; set; }

        public String County { get; set; }

        public String Telephone { get; set; }

        public String CardHolderFullName { get; set; }

        public String CardNumber { get; set; }

        public String CardCVC { get; set; }

        public int CardMonth { get; set; }

        public int CardYear { get; set; }

        public String CountryBilling { get; set; }

        public String AddressBilling { get; set; }

        public String Address2Billing { get; set; }

        public String TownBilling { get; set; }

        public String County2Billing { get; set; }

        public String GetFullName()
        {
            return FirstName + " " + LastName;

        }
    }
}
