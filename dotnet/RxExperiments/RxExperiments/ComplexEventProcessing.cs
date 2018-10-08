using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxExperiments
{
	public class ComplexEventProcessing
	{
		private string[] Stream = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" }; 

		public void Run()
		{
			StraightEnumerable();
			ObservableFromEnumerable();
			UseGrouping();
			UseWindowing();
			UseSlidingWindow();
			UseBuffering();
			UseSlidingBuffers();
		}

		private void StraightEnumerable()
		{
			Console.WriteLine("Straight enumerable:");
			foreach(var s in Stream)
				Console.WriteLine(s);
		}

		private void ObservableFromEnumerable()
		{
			Console.WriteLine("Observable from enumerable:");
			var src = Stream.ToObservable(NewThreadScheduler.Default);
			var res = src.Subscribe(Console.WriteLine);
			Thread.Sleep(100);
			res.Dispose();
		}

		private void UseGrouping()
		{
			Console.WriteLine("Using grouping:");
			var src = Stream.ToObservable(NewThreadScheduler.Default);
			var groups = from s in src
					  group s by s.Length;
			var res = groups.Subscribe(g =>
			{
				Console.WriteLine("New group created: " + g.Key);
				g.Subscribe(s => Console.WriteLine("  " + s + " is part of group " + g.Key));
			});
			Thread.Sleep(100);
			// TODO: Dispose of the inner subscriptions?
			res.Dispose();
		}

		private void UseWindowing()
		{
			Console.WriteLine("Using windowing:");
			var src = Stream.ToObservable(NewThreadScheduler.Default);
			var window = src.Window(3);
			var res = window.Subscribe(w =>
			{
				Console.WriteLine("New window created");
				w.Subscribe(s => Console.WriteLine("  " + s));
			});
			Thread.Sleep(100);
			// TODO: Dispose of the inner subscriptions?
			res.Dispose();
		}

		private void UseSlidingWindow()
		{
			Console.WriteLine("Using sliding window:");
			var src = Stream.ToObservable(NewThreadScheduler.Default);
			var window = src.Window(3, 2);
			var i = 0;
			var res = window.Subscribe(w =>
			                           {
				                           var j = i++;
										   Console.WriteLine("New window created: " + j);
										   w.Subscribe(s => Console.WriteLine("  " + s + " is in window " + j));
									   });
			Thread.Sleep(100);
			// TODO: Dispose of the inner subscriptions?
			res.Dispose();
		}

		private void UseBuffering()
		{
			Console.WriteLine("Using buffering:");
			var src = Stream.ToObservable(NewThreadScheduler.Default);
			var buffer = src.Buffer(3);
			var res = buffer.Subscribe(b =>
			{
				Console.WriteLine("New buffer created");
				foreach(var s in b)
					Console.WriteLine("  " + s);
			});
			Thread.Sleep(100);
			// TODO: Dispose of the inner subscriptions?
			res.Dispose();
		}

		private void UseSlidingBuffers()
		{
			Console.WriteLine("Using sliding buffers:");
			var src = Stream.ToObservable(NewThreadScheduler.Default);
			var buffer = src.Buffer(3, 2);
			var res = buffer.Subscribe(b =>
			{
				Console.WriteLine("New buffer created");
				foreach (var s in b)
					Console.WriteLine("  " + s);
			});
			Thread.Sleep(100);
			// TODO: Dispose of the inner subscriptions?
			res.Dispose();
		}
	}
}
