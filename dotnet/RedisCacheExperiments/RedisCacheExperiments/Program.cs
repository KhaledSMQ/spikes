using System;

namespace RedisCacheExperiments
{
	class Program
	{
		private const string Key1 = "key1";
		private const string Key2 = "key2";
		private const string Key3 = "key3";

		static void Main(string[] args)
		{
			Console.WriteLine("Starting...");

			var cache = new CacheClient();
			Console.WriteLine("Connecting...");
			cache.Connect();
			Console.WriteLine("Connected.");
			cache[Key1] = "value1";
			cache[Key2] = "value2";
			var value1 = cache[Key1];
			var value2 = cache[Key2];
			var value3 = cache[Key3];
			Console.WriteLine("Value in cache: {0} = {1}", Key1, value1);
			Console.WriteLine("Value in cache: {0} = {1}", Key2, value2);
			Console.WriteLine("Value in cache: {0} = {1}", Key3, value3);

			Console.WriteLine("Press <return> to quit.");
			Console.ReadLine();
		}
	}
}
