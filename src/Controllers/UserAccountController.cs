using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dream_holiday.Models;

namespace dream_holiday.Controllers
{
    [Route("user-account")]
    public class UserAccountController : Controller
    {
        public IActionResult Index()
        {
            var user = GetData();
            return View(user);
        }

        [HttpPost]
        public IActionResult Update(
            
            UserAccountModel formUser)
        {
            var user = GetData();
            return View("index", formUser);
        }

        private UserAccountModel GetData()
        {
            var user = new UserAccountModel();
            user.FirstName = "Petra";
            user.LastName = "Furkes";
            user.Email = "firstname.lastname@gmail.com";
            user.Paswoord = "pasword123";
            user.RetypePasword = "pasword123";
            user.BirthDay = DateTime.Now.AddYears(-20);
            user.BirthMonth = DateTime.Now.AddYears(-20);
            user.BirthYear = DateTime.Now.AddYears(-20);
            user.Country = "Ireland";
            user.Address = "Apt.25, Street 28";
            user.Address2 = "Dublin 1";
            user.Town = "Dublin";
            user.County = "Co. Dublin";
            user.Telephone = "123456789";
            user.FirstNameCard = "Petra";
            user.LastNameCard = "Furkes";
            user.CardNumber = "9842 4520 1111 2222";
            user.CardCVC = "228";
            user.CountryBilling = "Ireland";
            user.AddressBilling = "Apt. 25, street 28";
            user.Address2Billing = "Dublin 1";
            user.TownBilling = "Dublin";
            user.County2Billing = "Co. Dublin";

            return user;
        }
    }
}
