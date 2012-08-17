using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AssemblyWithConfiguration
{
	public class CustomSection : ConfigurationSection
	{
		public const string SectionName = "CustomSection";

		[ConfigurationProperty("SomeConfiguration", IsRequired = true)]
		public SomeConfigurationElement SomeConfiguration
		{
			get { return (SomeConfigurationElement)this["SomeConfiguration"]; }
		}
	}
}
