using System;
using System.Reactive.Linq;

namespace RxExperiments
{
	public class SimpleStringObservable
	{
		public void Run()
		{
			var xs = "Hello, World!".ToObservable();
			xs.Subscribe(Console.WriteLine);
		}
	}
}
