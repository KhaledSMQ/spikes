using System.Collections.Generic;
using System.Linq;

namespace SemanticLoggingExperiments
{
    public class FlatPayloadItem
    {
        public static object Create(object payloadItem)
        {
            // Arrays and dictionaries in the payload are always passed in as IEnumerable<object>.
            // Arrays are serializable directly, and so we let them flow through to the end of the
            // method. Dictionaries are passed in as a list of sub-dictionaries, each containing
            // two keys, "Key" and "Value". We therefore need to merge the sub-dictionaries into one
            // in order for it to serialize as a regular dictionary.
            var enumerable = payloadItem as IEnumerable<object>;
            if (enumerable != null)
            {
                var merged = MergeDictionariesInList(enumerable);
                if (merged != null)
                {
                    return merged;
                }
            }

            // Types marked as [EventData] will show up as instances of type EventPayload, which is internal
            // to System.Diagnostics.Tracing, but it also implements IDictionary<string, object>.
            // Note: Enumerating the items in the EventPayload fails and therefore we need to enumerate the
            // keys and retrieve the values individually
            var dictionary = payloadItem as IDictionary<string, object>;
            if (dictionary != null)
            {
                var cd = new Dictionary<string, object>();
                foreach (var key in dictionary.Keys)
                    cd[key] = dictionary[key];
                return cd;
            }

            return payloadItem;
        }

        private static IDictionary<string, object> MergeDictionariesInList(IEnumerable<object> enumerable)
        {
            IDictionary<string, object> result = null;
            foreach (var item in enumerable)
            {
                var dictionary = item as IDictionary<string, object>;
                if (dictionary != null)
                {
                    var keys = dictionary.Keys.ToArray();
                    var name = dictionary[keys[0]].ToString();
                    var value = dictionary[keys[1]];
                    if (result == null)
                        result = new Dictionary<string, object>();
                    result[name] = value;
                }
            }
            return result;
        }
    }
}
