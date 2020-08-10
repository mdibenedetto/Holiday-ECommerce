using Microsoft.AspNetCore.Mvc;

namespace dream_holiday.Controllers
{
    public class AboutController : Controller
    {
        // GET: /about
        public IActionResult Index()
        { 
            return View();
        }
    }
}
