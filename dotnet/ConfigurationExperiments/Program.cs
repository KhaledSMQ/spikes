using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ConfigurationExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting...");
			var p = new Program();
			p.Run();
			Console.WriteLine("Finished. Press any key to exit.");
			Console.ReadLine();
		}

		void Run()
		{
			ReadConfigurationFromExe();
			ReadConfigurationFromAssembly();
		}

		void ReadConfigurationFromExe()
		{
			var v1 = ConfigurationManager.AppSettings["Key1"];
			var v2 = ConfigurationManager.AppSettings["Key2"];
			var v3 = ConfigurationManager.AppSettings["Key3"];
			Console.WriteLine("Exe Key1 = " + v1);
			Console.WriteLine("Exe Key2 = " + v2);
			Console.WriteLine("Exe Key3 = " + v3);
		}

		void ReadConfigurationFromAssembly()
		{
			var assembly = new AssemblyWithConfiguration.SomeClass();
			assembly.ReadConfiguration();
		}
	}
}
