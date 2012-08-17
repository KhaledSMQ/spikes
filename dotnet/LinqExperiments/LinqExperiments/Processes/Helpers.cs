using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LinqExperiments.Processes
{
    public static class Helpers
    {
        public static void DisplayProcesses()
        {
            var processes =
            Process.GetProcesses()
              .Where(process => process.WorkingSet64 > 20 * 1024 * 1024)
              .OrderByDescending(process => process.WorkingSet64)
              .Select(process => new
              {
                  process.Id,
                  Name = process.ProcessName,
                  process.WorkingSet64
              });
            ObjectDumper.Write(processes);
        }

        public static void DisplayProcesses(Func<Process, Boolean> match)
        {
            var processes = new List<ProcessData>();
            foreach (var process in Process.GetProcesses())
            {
                if (match(process))
                {
                    processes.Add(new ProcessData
                    {
                        Id = process.Id,
                        Name = process.ProcessName,
                        Memory = process.WorkingSet64
                    });
                }
            }

            Console.WriteLine("Total memory: {0} MB",
                processes.TotalMemory() / 1024 / 1024);

            var top2Memory =
              processes
                .OrderByDescending(process => process.Memory)
                .Take(2)
                .Sum(process => process.Memory) / 1024 / 1024;
            Console.WriteLine(
              "Memory consumed by the two most hungry processes: {0} MB",
              top2Memory);
            var results = new
            {
                TotalMemory = processes.TotalMemory() / 1024 / 1024,
                Top2Memory = top2Memory,
                Processes = processes
            };
            ObjectDumper.Write(results, 1);
            ObjectDumper.Write(processes);
        }

        public static long TotalMemory(this IEnumerable<ProcessData> processes)
        {
            long result = 0;

            foreach (var process in processes)
                result += process.Memory;

            return result;
        }
    }
}
