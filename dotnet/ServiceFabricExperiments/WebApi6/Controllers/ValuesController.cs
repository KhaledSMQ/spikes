using System.Collections.Generic;
using System.Web.Http;

namespace WebApi6.Controllers
{
    [ServiceRequestActionFilter]
    public class ValuesController : ApiController
    {
        // https://alexmg.com/introducing-the-autofac-integration-for-service-fabric/
        // https://github.com/jakkaj/AutofacServiceFabricExample

        private IBusinessLogic BusinessLogic { get; }

        public ValuesController(IBusinessLogic businessLogic)
        {
            BusinessLogic = businessLogic;
        }

        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { BusinessLogic?.Hello() };
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
