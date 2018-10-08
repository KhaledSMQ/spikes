using System;
using System.Collections.Generic;
using System.Linq;

namespace RxExperiments
{
	public class SimpleEnumerable
	{
		public void Run()
		{
		    var ints = new[] {1, 2, 3, 4, 5, 6};

		    var list = GetList(ints);

		    var list2 = list.Take(3);

		    //Console.WriteLine(list.);
		}

	    public IEnumerable<int> GetList(IEnumerable<int> list)
	    {
            foreach (var i in list)
                yield return i * 2;
	    }
	}
}
