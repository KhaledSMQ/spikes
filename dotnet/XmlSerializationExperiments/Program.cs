using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjectModel;
using System.Xml.Serialization;
using System.IO;

namespace XmlSerializationExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting...");
			var p = new Program();
			p.Run();
			Console.WriteLine("Finished.");
			Console.ReadLine();
		}

		private void Run()
		{
			SerializeAndDeserialize();
		}

		private Farm BuildFarm()
		{
			var farm = new Farm();
			
			var rex = new Dog();
			rex.Name = "Rex";
			rex.Birthday = new DateTime(2009, 10, 22);
			farm.Animals.Add(rex);

			var tom = new Cat();
			tom.Name = "Tom";
			tom.Birthday = new DateTime(1940, 6, 15);
			farm.Animals.Add(tom);

			var jerry = new Mouse();
			jerry.Name = "Jerry";
			farm.Animals.Add(jerry);

			return farm;
		}

		private void SerializeAndDeserialize()
		{
			var farm = BuildFarm();
			var serialized = Serialize(farm);
			Console.WriteLine("Serialized form:");
			Console.WriteLine(serialized);
			//serialized = "<Farm name=\"my farm\">aaa</Farm>";
			var deserialized = Deserialize(serialized);
			Console.WriteLine("Deserialized form:");
			Console.WriteLine(deserialized);
		}

		private string Serialize(Farm farm)
		{
			var serializer = new XmlSerializer(typeof(Farm));
			var writer = new StringWriter();
			serializer.Serialize(writer, farm);
			return writer.ToString();
		}

		private Farm Deserialize(string serialized)
		{
			var reader = new StringReader(serialized);
			var serializer = new XmlSerializer(typeof(Farm));
			var farm = (Farm)serializer.Deserialize(reader);
			return farm;
		}
	}
}
