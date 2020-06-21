using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

//using = imports

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// namespace == package
// extends -> inherit
// implements -> Interface
namespace dream_holiday.Controllers
{
    public class FakeLoginController   : Controller
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logout()
        {
            return View();
        }



    }


}
