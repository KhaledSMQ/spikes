using System;

namespace ModuleOne
{
	public class ItemOneFactory
	{
		private const string NamePrefix = "Item One ";
		private const string IdPrefix = "I1Id";

		private int Count { get; set; }
		private Random RandomGenerator { get; set; }

		public ItemOneFactory()
		{
			Count = 1;
			RandomGenerator = new Random();
		}

		public ItemOne CreateItem()
		{
			var item = new ItemOne
			              	{
			              		Id = IdPrefix + Count,
			              		Name = NamePrefix + Count,
			              		Timestamp = DateTime.Now,
			              		Value = RandomGenerator.NextDouble() * RandomGenerator.Next(1000)
			              	};
			Count++;
			return item;
		}
	}
}
