using System.Configuration;
using System.Reflection;

namespace ConfigurationUtilities
{
	public class Configuration
	{
		private KeyValueConfigurationCollection AppSettings { get; set; }

		public Configuration()
		{
			var appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetCallingAssembly().Location);
			AppSettings = appConfig.AppSettings.Settings;
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
	}
}
