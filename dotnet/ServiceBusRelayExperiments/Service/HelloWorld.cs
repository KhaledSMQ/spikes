using System;

namespace Service
{
    public class HelloWorld : IHelloWorld
    {
        public string Hello()
        {
            return "Hello from the server... It is " + DateTime.Now.ToLongTimeString();
        }

        public string Goodbye()
        {
            return "Goodbye from the server... It is " + DateTime.Now.ToLongTimeString();
        }
    }
}
