using StackExchange.Redis;

namespace RedisCacheExperiments
{
	public class CacheClient
	{
		private ConnectionMultiplexer Connection { get; set; }
		private bool IsConnected { get; set; }

		public string this[string key]
		{
			get
			{
				var cache = Connection.GetDatabase();
				var value = cache.StringGet(key);
				return value;
			}
			set
			{
				var cache = Connection.GetDatabase();
				cache.StringSet(key, value);
			}
		}

		public void Connect()
		{
			var connection = ConnectionMultiplexer.Connect("genrtentitlements.redis.cache.windows.net,ssl=true,password=3ZhtKAyAo8JydTBpPK+TVFjIzr0COZ+kniOQv6Lua9E=");
			Connection = connection;
			IsConnected = true;
		}

		public void Disconnect()
		{
			Connection.Close();
			IsConnected = false;
		}
	}
}
