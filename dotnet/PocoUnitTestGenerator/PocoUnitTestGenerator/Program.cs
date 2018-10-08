using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Processing;

namespace PocoUnitTestGenerator
{
    class Program
    {
        private const string FileToProcess = "..\\..\\ApiEntitlement.cs";

        static void Main(string[] args)
        {
            Console.WriteLine("Running...");
            var p = new Processor(FileToProcess, null);
            p.Process();
            Console.WriteLine("Finished. Press <return> to close.");
            Console.ReadLine();
        }
    }
}
