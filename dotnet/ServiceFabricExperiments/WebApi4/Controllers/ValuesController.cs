using System.Collections.Generic;
using System.Web.Http;

namespace WebApi4.Controllers
{
    [ServiceRequestActionFilter]
    [Authorize]
    public class ValuesController : ApiController
    {
        private const int Value = 440;

        /// <summary>
        /// This method does a GET api/values
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value" + Value + 1, "value" + Value + 2 };
        }

        // GET api/values/5 
        public string Get(int id)
        {
            return "value";
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
    }
}
