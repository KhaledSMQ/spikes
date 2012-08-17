using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DateTimeExperiments
{
	class Program
	{
		private const string XmlDateFormat = "yyyy-MM-ddTHH:mm:ss.fffK";
		private readonly DateTime Date = new DateTime(1000, 2, 3, 4, 5, 6, 7, DateTimeKind.Utc);

		static void Main(string[] args)
		{
			Console.WriteLine("Starting...");
			var p = new Program();
			p.Run();
			Console.WriteLine("Finished. Press any key to exit.");
			Console.ReadLine();
		}

		private void Run()
		{
			WriteFormatted(Date, XmlDateFormat);
			WriteXml(Date);
			var date = DateTime.UtcNow;
			WriteFormatted(date, XmlDateFormat);
			WriteXml(date);
			var rounded = RoundToMilliseconds(date);
			WriteFormatted(rounded, XmlDateFormat);
			WriteXml(rounded);
		}

		private void WriteFormatted(DateTime date, string format)
		{
			Console.WriteLine(string.Format("Writing date {0} in format {1}:", date, format));
			Console.WriteLine(date.ToString(format));
		}

		private void WriteXml(DateTime date)
		{
			Console.WriteLine(string.Format("Writing date {0} in Xml:", date));
			var xml = ToXml(date);
			Console.WriteLine(xml);
		}

		private string ToXml(DateTime date)
		{
			var settings = new XmlWriterSettings
			{
				Indent = true,
				Encoding = new UTF8Encoding(false),
				ConformanceLevel = ConformanceLevel.Document
			};

			var writer = new StringWriter();
			using (var xmlWriter = XmlWriter.Create(writer, settings))
			{
				var serializer = new XmlSerializer(typeof(DateTime));
				serializer.Serialize(xmlWriter, date);
			}

			var serialized = writer.ToString();
			return serialized;
		}

		/*private DateTime FromXml(string xml)
		{
			var settings = new XmlWriterSettings
			{
				Indent = true,
				Encoding = new UTF8Encoding(false),
				ConformanceLevel = ConformanceLevel.Document
			};

			var writer = new StringWriter();
			using (var xmlWriter = XmlWriter.Create(writer, settings))
			{
				var serializer = new XmlSerializer(typeof(DateTime));
				serializer.Deserialize().Serialize(xmlWriter, date);
			}

			var serialized = writer.ToString();
			return serialized;
		}*/

		private DateTime RoundToMilliseconds(DateTime dateTime)
		{
			var excessTicks = dateTime.Ticks % 10000;
			var rounded = dateTime.Subtract(new TimeSpan(excessTicks));
			return rounded;
		}
	}
}
