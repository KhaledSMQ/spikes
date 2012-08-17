using System;
using System.Collections;

namespace LinqExperiments
{
    public static class Utility
    {
        public static void Dump(object set)
        {
            Console.WriteLine("Type: " + set.GetType());
            Console.WriteLine("Content value: " + set);
            if (set is IEnumerable)
                foreach (var n in set as IEnumerable)
                    Console.WriteLine(n);
            Console.WriteLine();
        }

    }
}
