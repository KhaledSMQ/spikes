using System.Configuration;

namespace ConfigurationExperiments.Manager
{
    public class CustomElement : ConfigurationElement
    {
        [ConfigurationProperty("someProp", IsKey = true, IsRequired = true)]
        public string SomeProp
        {
            get { return (string) base["someProp"]; }
            set { base["someProp"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public int Value
        {
            get { return (int)base["value"]; }
            set { base["value"] = value; }
        }
    }
}