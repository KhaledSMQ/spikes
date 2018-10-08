using System;
using System.ComponentModel;
using System.Configuration;
using System.Dynamic;
using ConfigurationExperiments.Xml;
using Microsoft.Framework.Configuration;
using Configuration = ConfigurationExperiments.Binder.Configuration;
using ConfigurationBuilder = Microsoft.Framework.Configuration.ConfigurationBuilder;

namespace ConfigurationExperiments
{
    class Program
	{
		static void Main(string[] args)
		{
		    var p = new Program();
		    //p.UseManagerOnCustomSection();
		    p.UseDynamic();
		    p.UseBinder();
		    p.UseManager();

			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}

        private void UseManagerOnCustomSection()
        {
            //var section = ConfigurationManager.GetSection("CustomSection") as Manager.CustomSection;
            //var customElement = section.Elements[0];
        }

        private void UseBinder()
        {
            var builder = new ConfigurationBuilder();
            var current = Environment.CurrentDirectory;
            builder.AddXmlFile(current + "\\ConfigurationExperiments.exe.config");
            var configuration = builder.Build();

            //var converter2 = TypeDescriptor.GetConverter(typeof (Type));
            //var converter = new TypeNameConverter();
            //var d = typeof (double);
            TypeDescriptor.AddAttributes(typeof (Type), new TypeConverterAttribute(typeof (TypeNameConverter)));
            var converter = TypeDescriptor.GetConverter(typeof(Type));
            var a = converter.ConvertFromInvariantString("System.Double");

            var model = new Configuration();
            configuration.Bind(model);
        }

        private void UseManager()
        {
            var cs = ConfigurationManager.ConnectionStrings["AAAConnectionString"].ConnectionString;
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var path = config.FilePath;
        }

        private void UseDynamic()
        {
            var dxml = Xml.ConfigurationBuilder.BuildFromDefaultConfigurationFile();
            var customSection = (DynamicElement) dxml.CustomSection;
            var cs = customSection.Bind<Binder.CustomSection>();
         }
    }
}
