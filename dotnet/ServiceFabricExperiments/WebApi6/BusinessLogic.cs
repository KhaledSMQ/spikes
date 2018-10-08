using System;

namespace WebApi6
{
    public class BusinessLogic : IBusinessLogic
    {
        public string Hello()
        {
            return "Hello, it is " + DateTime.Now;
        }
    }
}