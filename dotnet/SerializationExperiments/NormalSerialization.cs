using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace SerializationExperiments
{
	public class NormalSerialization
	{
		public string Serialized { get; set; }
		public object Deserialized { get; set; }

		public void Serialize(object o, Type rootType)
		{
			var serializer = new DataContractSerializer(rootType);
			var builder = new StringBuilder();
			var writer = XmlDictionaryWriter.Create(builder);
			serializer.WriteObject(writer, o);
			writer.Close();
			Serialized = builder.ToString();
		}

		public void Deserialize(object o, Type rootType)
		{
			var deserializer = new DataContractSerializer(rootType);
			var reader = XmlDictionaryReader.Create(new StringReader(Serialized));
			Deserialized = deserializer.ReadObject(reader);
		}
	}
}
