using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace ConfigurationExperiments.Xml
{
    public static class XElementExtensions
    {
        private static PluralizationService PluralizationService { get; set; }

        static XElementExtensions()
        {
            PluralizationService = PluralizationService.CreateService(CultureInfo.CurrentCulture);
        }

        public static dynamic ToDynamic(this XElement element)
        {
            var item = new DynamicElement();

            AddChildren(element, item);
            AddAttributes(element, item);
            AddValue(element, item);

            return item;
        }

        private static void AddChildren(XElement element, DynamicElement item)
        {
            if (element.HasElements)
            {
                var uniqueElements = element.Elements().Where(el => element.Elements().Count(el2 => el2.Name.LocalName.Equals(el.Name.LocalName)) == 1).ToList();
                var repeatedElements = element.Elements().Except(uniqueElements);

                foreach (var repeatedElementGroup in repeatedElements.GroupBy(re => re.Name.LocalName).OrderBy(el => el.Key))
                {
                    var list = new List<dynamic>();
                    foreach (var repeatedElement in repeatedElementGroup)
                        list.Add(ToDynamic(repeatedElement));

                    item[PluralizationService.Pluralize(repeatedElementGroup.Key)] = list;
                }

                foreach (var uniqueElement in uniqueElements.OrderBy(el => el.Name.LocalName))
                {
                    item[uniqueElement.Name.LocalName] = ToDynamic(uniqueElement);
                }
            }
        }

        private static void AddAttributes(XElement element, DynamicElement item)
        {
            if (element.Attributes().Any())
            {
                foreach (var attribute in element.Attributes())
                    item[attribute.Name.LocalName] = attribute.Value;
            }
        }

        private static void AddValue(XElement element, DynamicElement item)
        {
            if (!element.HasElements && !string.IsNullOrEmpty(element.Value))
            {
                item["Value"] = element.Value;
            }
        }
    }
}
