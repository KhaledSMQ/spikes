using System;
using System.Linq;
using MoreLinq;

namespace LinqExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
            var people = new []
            {
                new Person { Id = 1, Name = "Person1", Age = 10 },
                new Person { Id = 2, Name = "Person2", Age = 20 },
                new Person { Id = 3, Name = "Person3", Age = 30 },
                new Person { Id = 1, Name = "Person4", Age = 40 },
                new Person { Id = 4, Name = "Person1", Age = 50 },
                new Person { Id = 1, Name = "Person6", Age = 60 },
            };

		    var peopleDuplicateIds = people.GroupBy(u => u.Id).Where(g => g.Skip(1).Any()).SelectMany(g => g).ToList();
            var peopleDuplicateNames = people.GroupBy(u => u.Name).Where(g => g.Skip(1).Any()).SelectMany(g => g).ToList();

            Console.WriteLine("Done. Press any key to exit.");
			Console.ReadLine();
		}
	}
}
