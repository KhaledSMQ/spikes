using System.Collections.Generic;
using System.Linq;

namespace LinqExperiments.Syntax
{
    public class InnerObjects
    {
        private class Outer
        {
            public Inner Inner { get; set; }   
        }

        private class Inner
        {
            public IEnumerable<string> Strings { get; private set; }

            public Inner(IEnumerable<string> strings)
            {
                Strings = strings;
            }
        }

        public void Run()
        {
            var inner = new Inner(new [] {"a", "b", "c", "d", "b"});
            var inner2 = new Inner(new[] { "c", "d", "e", "f" });
            var inner3 = new Inner(new[] { "g", "h" });

            IEnumerable<Inner> inners = new[] {inner, inner2, inner3};

            var list =
                (from v in inner.Strings
                select v).Distinct();

            Utility.Dump(list);

            var l2 = list as IEnumerable<string>;
            Utility.Dump(l2);
        }
    }
}
