using System.Linq;
using System.Web.Mvc;
using MoodyWeb.Models;

namespace MoodyWeb.Controllers
{
    public class HistoryController : Controller
    {
		private readonly moodywebEntities _entities = new moodywebEntities();

        public ActionResult Index()
        {
	        var moods = from mood in _entities.tbl_moody
						orderby mood.time descending 
						select mood;

            return View(moods.Take(500));
        }
	}
}