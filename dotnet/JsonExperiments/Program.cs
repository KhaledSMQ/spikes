using System;
using System.Collections.Generic;

namespace JsonExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
			UseNameValuePairs();
			UseNameValuePairsWithCollections();
			UseNameValuePairsWithComplexObjects();
			Console.ReadKey();
		}

		private static void UseNameValuePairs()
		{
			var nvp = new NameValuePairs();
			nvp.SomeString = "Hello";
			nvp.SomeInt = 10;
			nvp.SomeDate = DateTime.UtcNow;
			var s = Serializer.Serialize(nvp);
			Console.WriteLine("Serialized:");
			Console.WriteLine(s);
			var d = Serializer.Deserialize<NameValuePairs>(s);
			Console.WriteLine("Deserialized:");
			Console.WriteLine("SomeString: {0}, SomeInt: {1}, SomeDate: {2}", d.SomeString, d.SomeInt, d.SomeDate);
		}

		private static void UseNameValuePairsWithCollections()
		{
			var nvp = new NameValuePairsWithCollections();
			nvp.SomeString = "Hello collections!";
			nvp.SomeInt = 20;
			nvp.SomeDate = DateTime.UtcNow.AddYears(1);
			nvp.SomeList = new List<string> {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};
			nvp.SomeDictionary = new Dictionary<string, int>{ {"K", 11}, {"L", 12}, {"M", 13}, {"N", 14}, {"O", 15}};
			var s = Serializer.Serialize(nvp);
			Console.WriteLine("Serialized:");
			Console.WriteLine(s);
			var d = Serializer.Deserialize<NameValuePairsWithCollections>(s);
			Console.WriteLine("Deserialized:");
			Console.WriteLine("SomeString: {0}, SomeInt: {1}, SomeDate: {2}", d.SomeString, d.SomeInt, d.SomeDate);
			Console.WriteLine("SomeList: {0}", string.Join(", ", d.SomeList));
			Console.WriteLine("SomeDictionary: {0}", string.Join(", ", d.SomeDictionary));
		}

		private static void UseNameValuePairsWithComplexObjects()
		{
			var nvp = new NameValuePairsWithComplexObjects();
			nvp.SomeString = "Hello complex!";
			nvp.SomeInt = 30;
			nvp.SomeDate = DateTime.UtcNow.AddYears(5);
			nvp.SomeList = new List<ComplexObjectBase>
				               {
					               new ComplexObjectBase { Name = "Complex1" },
								   new Derived1 { Name = "Derived11", Title = "D11" },
								   new Derived2 { Name = "Derived21", Value = 40 }
				               };
			nvp.SomeDictionary = new Dictionary<string, ComplexObjectBase>
				                     {
					                     { "P", new ComplexObjectBase { Name = "Complex2" } },
										 { "Q", new Derived1 { Name = "Derived12", Title = "D12" } },
										 { "R", new Derived2 { Name = "Derived22", Value = 50 } }
				                     };
			var s = Serializer.Serialize(nvp);
			Console.WriteLine("Serialized:");
			Console.WriteLine(s);
			var d = Serializer.Deserialize<NameValuePairsWithComplexObjects>(s);
			Console.WriteLine("Deserialized:");
			Console.WriteLine("SomeString: {0}, SomeInt: {1}, SomeDate: {2}", d.SomeString, d.SomeInt, d.SomeDate);
			Console.WriteLine("SomeList: {0}", string.Join(", ", d.SomeList));
			Console.WriteLine("SomeDictionary: {0}", string.Join(", ", d.SomeDictionary));
		}
	}
}
