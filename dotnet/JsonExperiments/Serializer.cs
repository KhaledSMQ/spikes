using Newtonsoft.Json;

namespace JsonExperiments
{
	public static class Serializer
	{
		public static string Serialize<T>(T payload)
		{
			var result = JsonConvert.SerializeObject(payload, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
			return result;
		}

		public static T Deserialize<T>(string json)
		{
			var result = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });
			return result;
		}
	}
}
