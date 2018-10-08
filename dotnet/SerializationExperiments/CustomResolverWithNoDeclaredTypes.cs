using System;
using System.Runtime.Serialization;
using System.Xml;

namespace SerializationExperiments
{
	public class CustomResolverWithNoDeclaredTypes : DataContractResolver
	{
		private readonly XmlDictionary _typeDictionary = new XmlDictionary();
		private const string Namespace = "http://the.namespace.com/";

		public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
		{
			if (typeNamespace.StartsWith(Namespace))
			{
				var ns = typeNamespace.Substring(Namespace.Length);
				var decodedName = XmlConvert.DecodeName(typeName);
				//if (decodedName == "InnerObject")
				//	decodedName = "InnerObject3";
				Type type = Type.GetType(ns + "." + decodedName, false);
				if (type != null)
				{
					return type;
				}
			}

			return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, null);
		}

		public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
		{
			//Console.WriteLine("TryResolveType: type = {0}, declaredType = {1}", type, declaredType);

			if (!knownTypeResolver.TryResolveType(type, declaredType, null, out typeName, out typeNamespace))
			{
				typeName = _typeDictionary.Add(XmlConvert.EncodeName(type.Name));
				typeNamespace = _typeDictionary.Add(Namespace + type.Namespace);
				return true;
			}

			return true;
		}
	}
}