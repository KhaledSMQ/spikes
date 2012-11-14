using System;

namespace ModuleTwo
{
	public class ItemTwoFactory
	{
		private const string CodePrefix = "Item Two code ";
		private const string DescriptionPrefix = "Item Two ";
		private const string IdPrefix = "I2Id";

		private int Count { get; set; }
		private Random RandomGenerator { get; set; }

		public ItemTwoFactory()
		{
			Count = 1;
			RandomGenerator = new Random();
		}

		public ItemTwo CreateItem()
		{
			var item = new ItemTwo
			           	{
			           		Id = IdPrefix + Count,
			           		Description = DescriptionPrefix + Count + " " + DateTime.Now,
			           		Code = CodePrefix + Count,
			           		Price = Convert.ToDecimal(RandomGenerator.NextDouble() * RandomGenerator.Next(1000))
			              	};
			Count++;
			return item;
		}
	}
}
