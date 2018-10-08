using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiStreaming.Controllers
{
	[RoutePrefix("api/values")]
    [EnableCors("*", "*", "*")]
	public class ValuesController : ApiController
	{
		// GET api/values
		[Route("")]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

	    [Route("csv/{name}")]
	    public HttpResponseMessage GetCsv(string name)
	    {
	        var result = new HttpResponseMessage(HttpStatusCode.OK);
	        var filedetails = GetCsvFileDetailsById(name);
	        var fullpath = filedetails.Item1;
	        var stream = new FileStream(fullpath, FileMode.Open);
	        result.Content = new StreamContent(stream);
	        result.Content.Headers.ContentType = new MediaTypeHeaderValue(filedetails.Item2);
	        return result;
	    }

        [Route("{name}")]
		public HttpResponseMessage Get(string name)
		{
			//name = "307_20140103_002026.jpg";
			var result = new HttpResponseMessage(HttpStatusCode.OK);
			var filedetails = GetFileDetailsById(name);
			var fullpath = filedetails.Item1;
			var stream = new FileStream(fullpath, FileMode.Open);
			result.Content = new StreamContent(stream);
			result.Content.Headers.ContentType = new MediaTypeHeaderValue(filedetails.Item2);
			return result;
		}

		// POST api/values
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		public void Delete(int id)
		{
		}

	    private Tuple<string, string> GetCsvFileDetailsById(string id)
	    {
	        var fullpath = HostingEnvironment.MapPath("~/") + id + ".csv";
	        var mediatype = "text/csv";
	        return new Tuple<string, string>(fullpath, mediatype);
	    }

        private Tuple<string, string> GetFileDetailsById(string id)
		{
			var fullpath = HostingEnvironment.MapPath("~/") + id + ".jpg";
			var mediatype = "image/jpeg";
			return new Tuple<string, string>(fullpath, mediatype);
		}
	}
}
