using System.Configuration;
using System.Reflection;

namespace ConfigurationUtilities
{
	public class ModuleConfiguration
	{
		public Configuration Configuration { get; private set; }
		private KeyValueConfigurationCollection AppSettings { get; set; }

		public static ModuleConfiguration GetModuleConfiguration()
		{
			var moduleConfiguration = GetModuleConfiguration(Assembly.GetCallingAssembly());
			return moduleConfiguration;
		}

		public static ModuleConfiguration GetModuleConfiguration(Assembly assembly)
		{
			var configuration = ConfigurationManager.OpenExeConfiguration(assembly.Location);
			var moduleConfiguration = new ModuleConfiguration();
			moduleConfiguration.Configuration = configuration;
			moduleConfiguration.AppSettings = configuration.AppSettings.Settings;
			return moduleConfiguration;
		}

		public ConfigurationSection GetSection(string sectionName)
		{
			var section = Configuration.GetSection(sectionName);
			return section;
		}

		public T GetSection<T>(string sectionName)
			where T : ConfigurationSection
		{
			var untypedSection = GetSection(sectionName);
			var section = untypedSection as T;
			return section;
		}

		public string this[string key]
		{
			get
			{
				string result = null;
				var element = AppSettings[key];
				if (element != null)
					result = element.Value;
				return result;
			}
		}

		protected ModuleConfiguration()
		{ }
	}
}
