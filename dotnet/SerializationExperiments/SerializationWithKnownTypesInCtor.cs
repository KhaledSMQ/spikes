using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace SerializationExperiments
{
	public class SerializationWithKnownTypesInCtor
	{
		public string Serialized { get; set; }
		public object Deserialized { get; set; }

		public void Serialize(object o, Type rootType, IEnumerable<Type> knownTypes)
		{
            var serializer = new DataContractSerializer(rootType, knownTypes);
            var builder = new StringBuilder();
            var writer = XmlDictionaryWriter.Create(builder);
            serializer.WriteObject(writer, o);
            writer.Close();
            Serialized = builder.ToString();
		}

		public void Deserialize(object o, Type rootType, IEnumerable<Type> knownTypes)
		{
			var deserializer = new DataContractSerializer(rootType, knownTypes);
			var reader = XmlDictionaryReader.Create(new StringReader(Serialized));
			Deserialized = deserializer.ReadObject(reader);
		}
	}
}
