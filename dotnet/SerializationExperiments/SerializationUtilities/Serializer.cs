using System;
using System.IO;
using System.Runtime.Serialization;

namespace SerializationUtilities
{
    public class Serializer
    {
        public SerializationConverterRegistry Converters { get; private set; }

        public Serializer()
        {
            Converters = new SerializationConverterRegistry();
        }

        public byte[] Serialize<T>(T entity)
        {
            return SerializeInternal(entity);
        }

        public T Deserialize<T>(byte[] data)
        {
            return DeserializeInternal<T>(data);
        }

        public string SerializeToString<T>(T entity)
        {
            byte[] bytes = Serialize(entity);
            return Convert.ToBase64String(bytes);
        }

        public T DeserializeFromString<T>(string data)
        {
            byte[] bytes = Convert.FromBase64String(data);
            return Deserialize<T>(bytes);
        }

        protected virtual byte[] SerializeInternal<T>(T entity)
        {
            var serializer = new NetDataContractSerializer();

            byte[] data;
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, entity);
                data = stream.ToArray();
            }

            return data;
        }

        protected virtual T DeserializeInternal<T>(byte[] data)
        {
            var serializer = new NetDataContractSerializer();

            object result;
            using (var stream = new MemoryStream(data))
            {
                result = serializer.ReadObject(stream);
            }

            var resultAsT = default(T);

            try
            {
                resultAsT = (T)result;
            }
            catch (InvalidCastException)
            {
                var rethrow = false;
                try
                {
                    resultAsT = ConvertInternal<T>(result.GetType(), result);
                }
                catch (Exception)
                {
                    rethrow = true;
                }

                if (rethrow)
                    throw;
            }

            return resultAsT;
        }

        protected virtual T ConvertInternal<T>(Type originalType, object original)
        {
            var converter = Converters.Get<T>(originalType);
            var converted = (T)converter.Convert(original);
            return converted;
        }
    }
}
