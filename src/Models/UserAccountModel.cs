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

        public String FirstName = "";
        public String LastName = "";
        public String Email = "";
        public DateTime BirthDay;
        public DateTime BirthMonth;
        public DateTime BirthYear;
        public String Paswoord = "";
        public String RetypePasword = "";
        public String Country = "";
        public String Address = "";
        public String Address2 = "";
        public String Town = "";
        public String County = "";
        public String Telephone = "";
        public String FirstNameCard = "";
        public String LastNameCard = "";
        public String CardNumber = "";
        public String CardCVC = "";
        public String CountryBilling = "";
        public String AddressBilling = "";
        public String Address2Billing = "";
        public String TownBilling = "";
        public String County2Billing = "";



        public String GetFullName()
        {
            return FirstName + " " + LastName;
       
        }

        public String GetFullNameCard()
        {
            return FirstNameCard + " " + LastNameCard;

        }
    }
}
