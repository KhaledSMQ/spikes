using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace SerializationExperiments
{
	public class SerializationWithResolverAndNoKnownTypes
	{
		public string Serialized { get; set; }
		public object Deserialized { get; set; }

		public void Serialize(object o, Type rootType, object rootInstance, DataContractResolver resolver)
		{
			var serializer = new DataContractSerializer(rootType, null, Int16.MaxValue, false, false, null, resolver);
			var builder = new StringBuilder();
			var writer = XmlDictionaryWriter.Create(builder);
			serializer.WriteObject(writer, rootInstance);
			writer.Close();
			Serialized = builder.ToString();
		}

		public void Deserialize(object o, Type rootType, object rootInstance, DataContractResolver resolver)
		{
			var deserializer = new DataContractSerializer(rootType, null, Int16.MaxValue, false, false, null, resolver);
			var reader = XmlDictionaryReader.Create(new StringReader(Serialized));
			Deserialized = deserializer.ReadObject(reader);
		}
	}
}
