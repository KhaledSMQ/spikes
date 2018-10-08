using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoodyWeb.Controllers
{
    public class VoteController : Controller
    {
        //
        // GET: /Vote/
        public ActionResult Index()
        {
            return View();
        }

	    public EmptyResult Up()
	    {
		    return null;
	    }

	    public EmptyResult Down()
	    {
		    return null;
	    }
	}
}