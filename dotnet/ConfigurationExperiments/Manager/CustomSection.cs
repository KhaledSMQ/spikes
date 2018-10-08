using System.Configuration;

namespace ConfigurationExperiments.Manager
{
    public class CustomSection : ConfigurationSection
    {
        /*public const string SectionName = "custom";

        [ConfigurationProperty("Elements", IsRequired = true)]
        [ConfigurationCollection(typeof(CustomElementCollection),
           AddItemName = "add",
           ClearItemsName = "clear",
           RemoveItemName = "remove")]
        public CustomElementCollection Elements
        {
            get { return (CustomElementCollection) this["Elements"]; }
        }*/
    }
}