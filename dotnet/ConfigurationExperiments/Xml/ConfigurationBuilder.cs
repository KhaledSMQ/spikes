using System.Configuration;
using System.Xml.Linq;

namespace ConfigurationExperiments.Xml
{
    public static class ConfigurationBuilder
    {
        public static dynamic BuildFromDefaultConfigurationFile()
        {
            var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
            var dxml = BuildFromXmlFile(path);
            return dxml;
        }

        public static dynamic BuildFromXmlFile(string path)
        {
            var xdoc = XDocument.Load(path);
            var dxml = xdoc.Root.ToDynamic();
            return dxml;
        }
    }
}
