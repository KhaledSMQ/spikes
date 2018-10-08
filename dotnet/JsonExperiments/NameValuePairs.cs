using System;
using System.Collections.Generic;

namespace JsonExperiments
{
	public class NameValuePairs
	{
		private IDictionary<string, object> Map { get; set; }
 
		public NameValuePairs()
		{
			Map = new Dictionary<string, object>();
		}

		public string SomeString
		{
			get { return (string)Map["Property1"]; }
			set { Map["Property1"] = value; }
		}

		public int SomeInt
		{
			get { return (int)Map["Property2"]; }
			set { Map["Property2"] = value; }
		}

		public DateTime SomeDate
		{
			get { return (DateTime)Map["Property3"]; }
			set { Map["Property3"] = value; }
		}
	}
}
