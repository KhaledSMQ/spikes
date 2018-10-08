using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace JsonExperiments
{
	public class NameValuePairsWithComplexObjects
	{
		private IDictionary<string, object> Map { get; set; }
 
		public NameValuePairsWithComplexObjects()
		{
			Map = new Dictionary<string, object>();
			Map["Property4"] = new List<ComplexObjectBase>();
			Map["Property5"] = new Dictionary<string, ComplexObjectBase>();
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

		public List<ComplexObjectBase> SomeList
		{
			get { return (List<ComplexObjectBase>)Map["Property4"]; }
			set { Map["Property4"] = value; }
		}

		public Dictionary<string, ComplexObjectBase> SomeDictionary
		{
			get { return (Dictionary<string, ComplexObjectBase>)Map["Property5"]; }
			set { Map["Property5"] = value; }
		}
	}

	public class ComplexObjectBase
	{
		public IDictionary<string, object> Properties { get; set; }

		public string Name
		{
			get { return Get<string>(); }
			set { Add(@value: value); }
		}

		public ComplexObjectBase()
		{
			Properties = new Dictionary<string, object>();
		}

		public override string ToString()
		{
			return string.Format("Name: {0}", Name);
		}

		protected virtual void Add([CallerMemberName] string key = "", object value = null)
		{
			Properties[key] = value;
		}

		protected virtual T Get<T>([CallerMemberName] string key = "")
		{
			var value = Properties[key];
			if (value is long && typeof(T) == typeof(int) ||
				value is double && typeof(T) == typeof(float) ||
				value is double && typeof(T) == typeof(decimal))
				value = Convert.ChangeType(value, typeof (T));

			return (T)value;
		}
	}

	public class Derived1 : ComplexObjectBase
	{
		public string Title
		{
			get { return Get<string>(); }
			set { Add(@value: value); }
		}

		public override string ToString()
		{
			return string.Format("{0}, Title: {1}", base.ToString(), Title);
		}
	}

	public class Derived2 : ComplexObjectBase
	{
		public int Value
		{
			get { return Get<int>(); }
			set { Add(@value: value); }
		}

		public override string ToString()
		{
			return string.Format("{0}, Value: {1}", base.ToString(), Value);
		}
	}
}
