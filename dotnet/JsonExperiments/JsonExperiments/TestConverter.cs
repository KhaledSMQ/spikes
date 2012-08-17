using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace JsonExperiments
{
    public class TestConverter : JavaScriptConverter
    {
        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /*public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (type == typeof(Product))
            {
                // Create the instance to deserialize into.
                var product = new Product();

                product.id = dictionary["id"] as string;
                product.Name = dictionary["Name"] as string;
                product.Date = dictionary["Date"] as DateTime;

                // Deserialize the ListItemCollection's items.
                ArrayList itemsList = (ArrayList)dictionary["List"];
                for (var i = 0; i < itemsList.Count; i++)
                    list.Add(serializer.ConvertToType<ListItem>(itemsList[i]));

                return product;
            }
            return null;
        }*/

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var product = obj as Product;

            if (product != null)
            {
                // Create the representation.
                var result = new Dictionary<string, object>();
                result["id"] = product.id;
                result["Name"] = product.Name;
                result["Date"] = product.Date;

                return result;
            }
            return new Dictionary<string, object>();
        }

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new ReadOnlyCollection<Type>(new List<Type>(new [] { typeof(Product) }));
            }
        }
    }
}
