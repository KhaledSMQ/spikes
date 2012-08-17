using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlSerialization;

namespace ObjectModel
{
	public class Farm : XmlSerializable
	{
		public IList<Animal> Animals { get; private set; }
		public string Name { get; private set; }

		public Farm()
		{
			Animals = new List<Animal>();
			Name = "my farm";
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.AppendFormat("Farm contains {0} animals:", Animals.Count).AppendLine();
			foreach (var animal in Animals)
			{
				builder.AppendFormat("  {0}: name: {1}, birthday: {2}", animal.GetType().Name, animal.Name, animal.Birthday.ToShortDateString()).AppendLine();
			}
			return builder.ToString();
		}

		protected override void Serialize()
		{
			AddAttribute("name", Name);
			foreach (var animal in Animals)
				AddElement(animal.GetType().Name, animal);
		}

		protected override void AttributeWasRead(string name, string value)
		{
		}

		protected override void AllAttributesRead()
		{
		}

		protected override object ElementWasFound(string name)
		{
			Animal animal = null;

			switch (name)
			{
				case "Dog":
					animal = new Dog();
					break;
				case "Cat":
					animal = new Cat();
					break;
				case "Mouse":
					animal = new Mouse();
					break;
			}

			Animals.Add(animal);

			return animal;
		}
	}
}
