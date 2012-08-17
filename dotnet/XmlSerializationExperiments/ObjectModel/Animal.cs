using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlSerialization;
using System.Xml.Serialization;

namespace ObjectModel
{
	public abstract class Animal : XmlSerializable
	{
		public Animal() { }

		public string Name { get; set; }
		public DateTime Birthday { get; set; }

		protected override void Serialize()
		{
			AddAttribute("name", Name);
			if (Birthday != DateTime.MinValue)
				AddElement("Birthday", Birthday.ToShortDateString());
		}

		protected override object ElementWasFound(string name)
		{
			if (name == "Birthday")
				Birthday = DateTime.Parse(ReadValue());

			return null;
		}
	}

	public class Dog : Animal
	{
		public Dog() { }
	}
	
	public class Cat : Animal
	{
		public Cat() { }
	}
	
	public class Mouse : Animal
	{
		public Mouse() { }
	}	
}
