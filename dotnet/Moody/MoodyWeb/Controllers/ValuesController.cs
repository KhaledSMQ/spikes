using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MoodyWeb.Models;

namespace MoodyWeb.Controllers
{
	public class ValuesController : ApiController
	{
		private readonly moodywebEntities _entities = new moodywebEntities();

		public IEnumerable<DateTime> GetZeros()
		{
			var moods = from mood in _entities.tbl_moody
						where mood.vote == 0
						orderby mood.time descending
						select mood.time;

			return moods.Take(500);
		}
	}
}