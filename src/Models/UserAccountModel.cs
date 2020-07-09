using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dream_holiday.Models
{
    public class UserAccountModel
    {
        public UserAccountModel() 
        { 
        }

        
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public DateTime BirthDay;
        public DateTime BirthMonth;
        public DateTime BirthYear;
        public String Pasword { get; set; }
        public String RetypePasword { get; set; }
        public String Country { get; set; }
        public String Address { get; set; }
        public String Address2 { get; set; }
        public String Town { get; set; }
        public String County { get; set; }
        public String Telephone { get; set; }
        public String CardHolderFullName { get; set; }
        public String CardNumber { get; set; }
        public String CardCVC { get; set; }
        public DateTime CardMonth;
        public DateTime CardYear;
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
