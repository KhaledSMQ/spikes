using System.Collections;
using System.Collections.Generic;

namespace EnumerableExperiments
{
	public class Things
	{
		private IList<string> _things = new List<string>();

		public Things()
		{
			_things.Add("one");
			_things.Add("two");
			_things.Add("three");
		}

		public IEnumerator GetEnumerator()
		{
			return _things.GetEnumerator();
		}
	}
}
