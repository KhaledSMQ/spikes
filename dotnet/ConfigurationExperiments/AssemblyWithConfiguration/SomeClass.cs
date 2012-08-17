using System;

namespace AssemblyWithConfiguration
{
	public class SomeClass
	{
		public void ReadConfiguration()
		{
			var configuration = ConfigurationUtilities.ModuleConfiguration.GetModuleConfiguration();
			var v1 = configuration["Key1"];
			var v2 = configuration["Key2"];
			var v3 = configuration["Key3"];
			Console.WriteLine("Assembly Key1 = " + v1);
			Console.WriteLine("Assembly Key2 = " + v2);
			Console.WriteLine("Assembly Key3 = " + v3);

			var section = configuration.GetSection<CustomSection>(CustomSection.SectionName);
			var someConfigElement = section.SomeConfiguration;
			var n = someConfigElement.Name;
			var v = someConfigElement.Value;
			Console.WriteLine("CustomSection/SomeConfiguration name = " + n + ", v = " + v);
		}
	}
}
