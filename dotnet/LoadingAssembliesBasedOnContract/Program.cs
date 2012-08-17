using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using BaseContract;

namespace LoadingAssembliesBasedOnContract
{
	class Program
	{
		static void Main(string[] args)
		{
			var p = new Program();
			Console.WriteLine("Starting...");
			p.Run();
			Console.WriteLine("Finished. Press any key to continue.");
			Console.ReadLine();
		}

		private void Run()
		{
			var assemblies = GetAssembliesInFolder();
			var sw = new Stopwatch();
			var filtered = FilterAssemblies(assemblies);
			sw.Stop();
			WriteTiming("Filtered types", sw);
			Console.WriteLine("These types implement IModule:");
			foreach(var type in filtered)
			{
				Console.WriteLine(type.FullName);
			}

			var moduleInstances = new List<IModule>();
			sw = new Stopwatch();
			foreach(var type in filtered)
			{
				var instance = Activator.CreateInstance(type) as IModule;
				moduleInstances.Add(instance);
			}
			sw.Stop();
			WriteTiming("Instantiated modules", sw);
		} 

		private IList<Type> FilterAssemblies(IList<Assembly> assemblies)
		{
			var modules = new List<Type>();
			foreach(var assembly in assemblies)
			{
				modules.AddRange(GetAssemblyModules(assembly));
			}
			return modules;
		}

		private IList<Type> GetAssemblyModules(Assembly assembly)
		{
			var allTypes = assembly.GetExportedTypes();
			var moduleTypes = new List<Type>();
			foreach(var type in allTypes)
			{
				if (type.IsSubclassOf(typeof(IModule)))
					moduleTypes.Add(type);
			}

			return moduleTypes;
		}

		private IList<Assembly> GetAssembliesInFolder()
		{
			var assemblies = new List<Assembly>();
			var filenames = GetAssemblyFilenamesInFolder();
			Console.WriteLine("Found these assemblies:");
			foreach (var filename in filenames)
				Console.WriteLine(filename);
			var sw = new Stopwatch();
			foreach(var filename in filenames)
			{
				var assembly = Assembly.LoadFrom(filename);
				assemblies.Add(assembly);
			}
			sw.Stop();
			WriteTiming("Loaded assemblies", sw);
			return assemblies;
		}

		private IList<string> GetAssemblyFilenamesInFolder()
		{
			var sw = new Stopwatch();
			sw.Start();
			var filenames = Directory.GetFiles(".", "*.dll").ToList();
			sw.Stop();
			WriteTiming("Read assembly filenames", sw);
			return filenames;
		}

		private void WriteTiming(string message, Stopwatch sw)
		{
			Console.WriteLine(message + " in " + sw.ElapsedTicks + " ticks.");
		}
	}
}
