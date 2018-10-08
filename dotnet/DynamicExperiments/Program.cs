using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
			var car1 = new Car {Brand = "b1", Model = "m1"};

			PrintCar(car1);

			dynamic car2 = new object();
			car2.Brand = "b2";
			car2.Mondel = "m2";

			PrintCar(car2);

			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}

		private static void PrintCar(Car car)
		{
			Console.WriteLine("Car of brand {0} and model {1}.", car.Brand, car.Model);
		}

		private static void PrintCar(dynamic car)
		{
			Console.WriteLine("Dynamic car of brand {0} and model {1}.", car.Brand, car.Model);
		}
	}
}
