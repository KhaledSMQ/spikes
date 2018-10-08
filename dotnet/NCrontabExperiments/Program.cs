using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCrontab;

namespace NCrontabExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private void Run()
        {
            var c = "0 12 * */2 Mon";
            var o = CreateOccurrences(c);
            var s = o.Take(20);
            Output(s);
        }

        private IEnumerable<DateTime> CreateOccurrences(string cron /* s M h d m w */)
        {
            var schedule = CrontabSchedule.Parse("2/25 13-14 * * *");
            var start = DateTime.Now;
            var end = start.AddYears(1);
            var occurrences = schedule.GetNextOccurrences(start, end);
            return occurrences;
        }

        private void Output(IEnumerable<DateTime> occurrences)
        {
            foreach (var occurrence in occurrences)
            {
                Console.WriteLine(occurrence);
            }
        }
    }
}
