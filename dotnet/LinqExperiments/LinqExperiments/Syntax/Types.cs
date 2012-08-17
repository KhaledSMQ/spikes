using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqExperiments.Syntax
{
    public class Types
    {
        private readonly int[] _numbers = new[] { 6, 3, 42, 14 };

        public void Run()
        {
            var filtered =
                from n in _numbers
                where n > 5 
                select n;

            Utility.Dump(filtered);

            var sorted =
                from n in _numbers
                where n > 5
                orderby n
                select n;

            Utility.Dump(sorted);
        }
    }
}
