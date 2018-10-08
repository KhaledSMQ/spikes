using System;
using ClassLibrary1;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			var c = new Class1();
			c.ExecuteStaticMethodInPhp();
			c.TestErrorsInPhp();
			Console.ReadLine();
		}
	}
}
