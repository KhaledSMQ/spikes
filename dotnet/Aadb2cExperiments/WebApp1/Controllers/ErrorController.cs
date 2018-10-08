using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApp1.Controllers
{
    //[Authorize]
    public class ErrorController : Controller
    {
        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View("Error");
        }
    }
}
