using System;
using System.Web.Script.Serialization;

namespace JsonExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var p = new Program();
            p.Run();
            Console.WriteLine("Finished.");
            Console.ReadLine();
        }

        private void Run()
        {
            var converter = new TestConverter();
        }
    }
}
