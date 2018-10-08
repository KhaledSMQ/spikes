using System.Collections.Generic;
using System.Web.Http;
using SpikesCo.Platform.Entitlements.EntitlementsModule;

namespace WebApi5.Controllers
{
    [Authorize]
    [CheckEntitlements(Application = "RT", Service = "Entity Data", Controller = "Data")]
    [ServiceRequestActionFilter]
    public class ValuesController : ApiController
    {
        private const int Value = 550;

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
