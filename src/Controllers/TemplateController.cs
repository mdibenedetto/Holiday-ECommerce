using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dream_holiday.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// routing reference
// https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/routing?view=aspnetcore-3.1


namespace dream_holiday.Controllers
{
    [Route("template")]
    public class TemplateController : Controller
    {
        
        public String var1 = "this is var";

        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.var1  = "this is a ViewBag var1";


            return View(new TemplateModel());
        }

 
        [Route("/template/dummy")] 
        public IActionResult Dummy()
        {
            return View("index",  new TemplateModel(prop1:  "Dummy string"));
        }

        [Route("dummy2")]
        public IActionResult Dummy2()
        {
            return View("index", new TemplateModel(prop1: "222 Dummy"));
        }

        [Route("dummy3/{index}")]
        public IActionResult Dummy2(String index)
        {
            return View("index", new TemplateModel(prop1: "index: " + index));
        }

        [Route("dummy4/{id?}")]
        public IActionResult Dummy3(String id)
        {
            return View("index", new TemplateModel(prop1: "index: " + id));
        }
    }
}
