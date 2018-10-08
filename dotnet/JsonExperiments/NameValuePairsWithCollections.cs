using System;
using System.Collections.Generic;

namespace JsonExperiments
{
	public class NameValuePairsWithCollections
	{
		private IDictionary<string, object> Map { get; set; }
 
		public NameValuePairsWithCollections()
		{
			Map = new Dictionary<string, object>();
			Map["Property4"] = new List<string>();
			Map["Property5"] = new Dictionary<string, int>();
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

		public List<string> SomeList
		{
			get { return (List<string>)Map["Property4"]; }
			set { Map["Property4"] = value; }
		}

		public Dictionary<string, int> SomeDictionary
		{
			get { return (Dictionary<string, int>)Map["Property5"]; }
			set { Map["Property5"] = value; }
		}
	}
}
