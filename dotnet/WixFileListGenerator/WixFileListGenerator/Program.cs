using System;
using System.IO;
using System.Text;

namespace WixFileListGenerator
{
	class Program
	{
		private const string Template = "<File Id=\"{0}\" Name=\"{0}\" DiskId=\"1\" Source=\"$(var.SourcePath){0}\" Vital=\"yes\"/>";

		static void Main(string[] args)
		{
			var path = ".";

			if (args.Length > 0)
				path = args[0];
			var filenames = Directory.EnumerateFiles(path);

			var builder = new StringBuilder();

			foreach (var filename in filenames)
			{
				var sanitized = filename;
				var lastSlashPos = filename.LastIndexOf('\\');
				if (lastSlashPos >= 0)
					sanitized = filename.Substring(lastSlashPos + 1);

				var item = string.Format(Template, sanitized);
				builder.AppendLine(item);
			}

			Console.WriteLine(builder);
		}
	}
}
