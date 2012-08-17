using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace CompressionExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            var program = new Program();
            program.Run();
            Console.WriteLine("Finished.");
            Console.ReadLine();
        }

        private void Run()
        {
        	var files = GetFiles("*.xml");
			foreach (var file in files)
			{
				var target = GetTargetFile(file);
				CreateZipFile(target, file);
			}

        	var report = BuildReport();
        	var printable = GetPrintableReport(report);
        	SaveReport(printable);
        }

		private IEnumerable<FileDetails> BuildReport()
		{
			var details = new List<FileDetails>();
			var xmlFiles = GetFiles("*.xml");
			foreach(var xmlFile in xmlFiles)
				details.Add(new FileDetails { XmlFileName = GetFileNameMinusPath(xmlFile) });
			var zipFiles = GetFiles("*.zip").ToList();
			for (var i = 0; i < zipFiles.Count; ++i)
				details[i].ZipFileName = GetFileNameMinusPath(zipFiles[i]);

			GetFileDetails(details);

			return details;
		}

		private void SaveReport(string report)
		{
			File.WriteAllText(@"report.txt", report);
		}

		private string GetPrintableReport(IEnumerable<FileDetails> details)
		{
			var builder = new StringBuilder();
			foreach(var entry in details)
			{
				var rate = 100.0 * entry.ZipSize / entry.XmlSize;
				var factor = 1.0 * entry.XmlSize / entry.ZipSize;
				builder.AppendLine(string.Format("{0},{1},{2},{3},{4},{5}", entry.XmlFileName, entry.ZipFileName, entry.XmlSize,
												entry.ZipSize, rate.ToString("0.00"), factor.ToString("0.00")));
			}
			return builder.ToString();
		}

		private void GetFileDetails(IEnumerable<FileDetails> files)
		{
			var list = files.ToList();
			foreach(var entry in list)
			{
				var xmlInfo = new FileInfo(entry.XmlFileName);
				var zipInfo = new FileInfo(entry.ZipFileName);
				entry.XmlSize = xmlInfo.Length;
				entry.ZipSize = zipInfo.Length;
			}
		}

		private IEnumerable<string> GetFiles(string pattern)
		{
			var files = Directory.GetFiles(Directory.GetCurrentDirectory(), pattern);
			Array.Sort(files);
			return files;
		}

		private string GetFileNameMinusPath(string file)
		{
			var parts = file.Split('\\');
			var filename = parts[parts.Length - 1];
			return filename;
		}

		private string GetTargetFile(string sourceFile)
		{
			var filename = GetFileNameMinusPath(sourceFile);
			var name = filename.Split('.')[0];
			filename = name + ".zip";
			return filename;
		}

		private void CreateZipFile(string targetFile, string sourceFile)
		{
			using (var zip = new ZipFile())
			{
				zip.AddFile(sourceFile, string.Empty);
				zip.Save(targetFile);
			}
		}
    }
}
