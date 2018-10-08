using Newtonsoft.Json;

namespace SemanticLoggingExperiments
{
    public class PayloadItemFormatter
    {
        public static string Format(object payloadItem)
        {
            var flattened = FlatPayloadItem.Create(payloadItem);
            var serialized = Serialize(flattened);
            return serialized;
        }

        private static string Serialize(object obj)
        {
            string result;
            switch (obj.GetType().Name)
            {
                case "Boolean":
                case "Byte":
                case "Int16":
                case "Int32":
                case "Int64":
                case "Single":
                case "Double":
                case "DateTime":
                case "Char":
                case "String":
                    result = obj.ToString();
                    break;
                default:
                    result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });
                    break;
            }
            return result;
        }
    }
}
