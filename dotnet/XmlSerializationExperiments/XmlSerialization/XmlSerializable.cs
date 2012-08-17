using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace XmlSerialization
{
	public class XmlSerializable : IXmlSerializable
	{
		private IList<KeyValuePair<string, string>> Attributes { get; set; }
		private IList<KeyValuePair<string, object>> Elements { get; set; }

		public XmlSerializable()
		{
			Attributes = new List<KeyValuePair<string, string>>();
			Elements = new List<KeyValuePair<string, object>>();
		}

		public void AddAttribute(string name, string value)
		{
			var pair = new KeyValuePair<string, string>(name, value);
			Attributes.Add(pair);
		}

		public void AddElement(string name, object value)
		{
			var pair = new KeyValuePair<string, object>(name, value);
			Elements.Add(pair);
		}

		public XmlSchema GetSchema()
		{
			return null;
		}

		public virtual void ReadXml(XmlReader reader)
		{
			reader.MoveToContent();
			if (reader.HasAttributes)
				while (reader.MoveToNextAttribute())
					AttributeWasRead(reader.Name, reader.Value);

			AllAttributesRead();

			var isEmpty = reader.IsEmptyElement;
			if (!isEmpty)
			{
				while (reader.Read())
				{
					var nodeType = reader.NodeType;
					switch (nodeType)
					{
						case XmlNodeType.Text:
							var content = reader.ReadContentAsString();
							ContentWasRead(string.Empty, content);
							break;
						case XmlNodeType.Element:
							var name = reader.Name;
							ProcessElement(name, reader);
							break;
					}
				}
			}
		}

		public virtual void WriteXml(XmlWriter writer)
		{
			Serialize();

			foreach (var pair in Attributes)
				writer.WriteAttributeString(pair.Key, pair.Value);
			foreach (var pair in Elements)
			{
				var name = pair.Key;
				var value = pair.Value;
				var valueAsXmlSerializable = value as XmlSerializable;
				if (valueAsXmlSerializable != null)
				{					
					writer.WriteStartElement(name); 
					valueAsXmlSerializable.WriteXml(writer);
					writer.WriteEndElement();
				}
				else
				{
					var valueAsString = value as string;
					if (valueAsString != null)
						writer.WriteElementString(pair.Key, valueAsString);
				}
			}			
		}

		protected virtual void Serialize()
		{
		}

		protected virtual void AttributeWasRead(string name, string value)
		{
		}

		protected virtual void AllAttributesRead()
		{
		}

		protected virtual void ContentWasRead(string name, string content)
		{
		}

		protected virtual object ElementWasFound(string name)
		{
			return null;
		}

		protected virtual void ElementWasProcessed(string name, object element)
		{
		}

		protected virtual string ReadValue()
		{
			CurrentInnerReader.MoveToContent();
			var value = CurrentInnerReader.ReadElementContentAsString();
			return value;
		}

		protected virtual int ReadValue(int dummy)
		{
			CurrentInnerReader.MoveToContent();
			var value = CurrentInnerReader.ReadElementContentAsInt();
			return value;
		}

		private XmlReader CurrentInnerReader { get; set; }

		private void ProcessElement(string name, XmlReader reader)
		{
			CurrentInnerReader = reader.ReadSubtree();

			var child = ElementWasFound(name);
			
			if (child != null)
			{
				var childAsXmlSerializable = child as IXmlSerializable;
				if (childAsXmlSerializable != null)
					childAsXmlSerializable.ReadXml(CurrentInnerReader);
			}

			CurrentInnerReader.Close();
			ElementWasProcessed(name, child);
		}

		//private void 
	}
}
