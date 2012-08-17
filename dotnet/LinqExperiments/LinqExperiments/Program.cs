using System;
using LinqExperiments.Syntax;

namespace LinqExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            RunInnerObjects();
            Console.ReadLine();
        }

        static void DisplayProcesses()
        {
            Processes.Helpers.DisplayProcesses();
        }

        static void RunTypes()
        {
            var t = new Types();
            t.Run();
        }

        static void RunInnerObjects()
        {
            var i = new InnerObjects();
            i.Run();
        }
    }
}
